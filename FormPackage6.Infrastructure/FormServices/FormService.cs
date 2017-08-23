using FormPackage6.Core.Alias;
using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using Skybrud.Umbraco.GridData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using umbraco.MacroEngines;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace FormPackage6.Infrastructure.FormServices
{
    public class FormService : IFormService
    {
        private IMediaService mediaService;
        private UmbracoHelper umbracoHelper;
        private IFieldService fieldService;
        private ILogService logService;
        public FormService
            (
                IMediaService mediaService,
                UmbracoHelper umbracoHelper,
                IFieldService fieldService,
                ILogService logService
            )
        {
            this.mediaService = mediaService;
            this.umbracoHelper = umbracoHelper;
            this.fieldService = fieldService;
            this.logService = logService;
        }
        public List<Form> GetForms()
        {
            List<Form> forms = new List<Form>();
            foreach (IPublishedContent root in umbracoHelper.ContentAtRoot())
            {

                if (!root.DocumentTypeAlias.Equals(NodeAlias.FormCollection)) continue;
                var query = root.Children.Where(n => n.DocumentTypeAlias.Equals(NodeAlias.FormNode)).Select(cn => new Form() { Id = cn.Id, Name = cn.Name });
                return query.ToList();
            }

            return forms;
        }
        public Form GetByNodeId(int Id)
        {
            Form form = new Form();
            IPublishedContent formNode = umbracoHelper.TypedContent(Id);
            if (formNode == null) return null;
            GridDataModel formGrid = formNode.GetPropertyValue<GridDataModel>(PropertyAlias.FormGrid);

            form.Id = formNode.Id;
            form.Name = formNode.Name;
            form.ButtonText = formNode.HasValue(PropertyAlias.SubmitButton) ? formNode.GetPropertyValue<string>(PropertyAlias.SubmitButton) : string.Empty;
            form.IsSaveLog = fieldService.GetValueAsBoolean(formNode.HasValue(PropertyAlias.SaveLog) ? formNode.GetPropertyValue<string>(PropertyAlias.SaveLog) : string.Empty);
            form.LogName = fieldService.PropertyValueAsFields(formNode.HasValue(PropertyAlias.LogName) ? formNode.GetPropertyValue<string>(PropertyAlias.LogName) : string.Empty).FirstOrDefault();
            form.IsEnableSpamFilter = formNode.HasValue(PropertyAlias.SpamFilter) ? formNode.GetPropertyValue<bool>(PropertyAlias.SpamFilter) : false;
            form.DataModel = formGrid;
            form.Fields = fieldService.GridControlsAsFields(formGrid.GetAllControls());
            form.Templates = GetEmailTemplates(formNode);
            form.SuccessMessage = formNode.HasValue(PropertyAlias.SuccessMessage) ? formNode.GetPropertyValue<string>(PropertyAlias.SuccessMessage) : string.Empty;
            form.ErrorMessage = formNode.HasValue(PropertyAlias.ErrorMessage) ? formNode.GetPropertyValue<string>(PropertyAlias.ErrorMessage) : string.Empty;
            return form;
        }
        public void SaveUploadFiles(ref Form form, List<FileViewModel> uploadedFiles)
        {
            FileViewModel fileModel;
            string mediaType;
            int mediaFolderId;
            IMedia media;

            foreach (var field in form.Fields.Where(n => n.FieldType.Type.Equals(PropertyAlias.Upload) || n.FieldType.Type.Equals(PropertyAlias.DragDropUpload)))
            {
                try
                {
                    foreach (var item in uploadedFiles)
                    {
                        string[] itemNameSplited = item.name.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                        if (itemNameSplited[1] == field.Id)
                        {
                            fileModel = item;
                            mediaType = fileModel.file.ContentType.Contains("image") ? "Image" : "File";
                            mediaFolderId = -1;

                            if (field.MediaFolder != null)
                                mediaFolderId = field.MediaFolder.Id != 0 ? field.MediaFolder.Id : -1;

                            media = mediaService.CreateMedia(fileModel.file.FileName, mediaFolderId, mediaType);

                            media.SetValue("umbracoFile", fileModel.file);
                            mediaService.Save(media);

                            if (field.FieldType.Type.Equals(PropertyAlias.DragDropUpload))
                            {
                                field.Value += string.Format(Environment.NewLine + " {0}", GetFullMediaPath(media.Id)); //GetFullMediaPath(media.Id);
                            }
                            else
                            {
                                field.Value = GetFullMediaPath(media.Id);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    field.Value = string.Empty;
                    LogHelper.Error<string>("Cannot save upload files ", ex);
                }
            }
        }
        public Result Submit(FormCollection formCollection, List<FileViewModel> uploadedFiles)
        {
            Result result = new Result();

            result = SpamFilter(formCollection);
            if (result.Status == "spam")
            {
                return result;
            }

            Form form = GetByNodeId(Convert.ToInt32(formCollection[PropertyAlias.FormId]));
            form.Fields = GetFieldsValue(form.Fields, formCollection);
            result = Validate(form, uploadedFiles);

            if (result.Status == "success")
            {
                form.Templates = UpdatingTemplate(form.Templates, form.Fields);
                form.IPAddress = HttpContext.Current.Request.UserHostAddress;
                form.PostUrl = formCollection[PropertyAlias.PostUrl];
                SaveUploadFiles(ref form, uploadedFiles);

                System.Threading.Tasks.Task t = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    SendEmail(form);
                });

                logService.CreateLog(form);
                result.Message = form.SuccessMessage;
            }
            else
            {
                result.Message = form.ErrorMessage;
            }

            return result;
        }
        public Result Validate(Form formModel, List<FileViewModel> uploadedFiles)
        {
            Result result = new Result
            {
                Status = "success",
                Errors = new List<FieldError>()
            };
            bool isValid = true;
            ActionMessage actionMessage = new ActionMessage();
            actionMessage.Message = new Message();
            actionMessage.IsSuccess = isValid;
            foreach (Field field in formModel.Fields)
            {
                field.IsRequiredValid = true;
                field.IsFormatValid = true;
                field.IsContainHTML = false;

                if (field.Mandatory)
                {
                    if (field.FieldType.Type.Equals(PropertyAlias.DragDropUpload) ||
                        field.FieldType.Type.Equals(PropertyAlias.Upload))
                    {
                        foreach (var uploadFile in uploadedFiles)
                        {
                            if (field.Id != uploadFile.name.Split('[', ']')[1])
                            {
                                field.IsRequiredValid = false;
                                isValid = false;
                            }
                            else
                            {
                                field.IsRequiredValid = true;
                                isValid = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(field.Value))
                        {
                            field.IsRequiredValid = false;
                            isValid = false;
                        }
                    }
                }

                /***************************** Refactor ***********************/
                // use else if 

                if (field.FieldType.Type.Equals(PropertyAlias.Email))
                {
                    if (!string.IsNullOrEmpty(field.Value))
                    {
                        try
                        {
                            MailAddress address = new MailAddress(field.Value);
                        }
                        catch (FormatException)
                        {
                            field.IsFormatValid = false;
                            isValid = false;
                        }
                    }
                }

                if (field.FieldType.Type.Equals(PropertyAlias.Phone))
                {
                    if (!string.IsNullOrEmpty(field.Value))
                    {
                        Regex pPattrn = new Regex("[0-9]+");
                        if (!pPattrn.IsMatch(field.Value))
                        {
                            field.IsFormatValid = false;
                            isValid = false;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(field.Value))
                {
                    if (field.Value.Contains(">") || field.Value.Contains("<"))
                    {
                        field.IsContainHTML = true;
                        isValid = false;
                    }

                }
                if (field.FieldType.Type.Equals(PropertyAlias.HoneyPot))
                {
                    if (!string.IsNullOrEmpty(field.Value))
                    {
                        field.IsFormatValid = false;
                        isValid = false;
                    }
                }
            }
            if (!isValid)
            {
                result.Status = "fail";

                // Why loop again

                foreach (Field field in formModel.Fields)
                {
                    if (!(field.IsRequiredValid && field.IsFormatValid))
                    {
                        string errorType = !field.IsRequiredValid ? "require" : "format";
                        string errorMessageMandatory = string.Empty;
                        string errorMessageValidation = string.Empty;

                        errorMessageMandatory = !string.IsNullOrEmpty(field.MessageMandatory) ? field.MessageMandatory : string.Empty;
                        errorMessageValidation = !string.IsNullOrEmpty(field.MessageInvalidFormat) ? field.MessageInvalidFormat : string.Empty;

                        FieldError fieldError = new FieldError
                        {
                            ErrorType = errorType,
                            FieldName = field.Name.ToString(),
                            FieldId = field.Id.ToString(),
                            ErrorMessageMandatory = errorMessageMandatory,
                            ErrorMessageValidation = errorMessageValidation
                        };
                        result.Errors.Add(fieldError);
                    }
                }
                result.Message = "fail";
            }
            else
            {
                actionMessage.IsSuccess = true;
                result.Message = "success";
            }
            return result;
        }
        public void SendEmail(Form formModel)
        {
            foreach (EmailTemplate template in formModel.Templates)
            {
                try
                {
                    string emailBody = template.Body;

                    foreach (Field field in formModel.Fields)
                    {
                        ReplaceFieldValue(ref emailBody, field);
                    }
                    MailMessage mailMessage = new MailMessage();

                    template.Body = emailBody;
                    mailMessage.Subject = template.Subject;
                    mailMessage.From = new MailAddress(template.From, template.SenderName);
                    template.To.ForEach(m => mailMessage.To.Add(m.MailAddress));
                    template.Cc.ForEach(m => mailMessage.CC.Add(m.MailAddress));
                    template.Bcc.ForEach(m => mailMessage.Bcc.Add(m.MailAddress));
                    mailMessage.Body = emailBody;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(GetType(), "Error on SendEmail", ex);
                }
            }
        }
        private Result SpamFilter(FormCollection formCollection)
        {
            Result result = new Result();

            if (!string.IsNullOrEmpty(formCollection[PropertyAlias.HoneyPot]))
            {
                result.Status = "spam";
            }
            return result;
        }
        private void ReplaceFieldValue(ref string emailBody, Field field)
        {
            Regex regex = new Regex("<ins(.*)</ins>");
            var matches = regex.Matches(emailBody);

            foreach (Match match in matches)
            {
                if (match.Value.IndexOf(field.Id) > -1)
                {
                    string value = string.Empty;
                    if (!string.IsNullOrEmpty(field.Value))
                        value = field.Value.Replace(Environment.NewLine, "<br />");

                    emailBody = emailBody.Replace(match.Value, value);
                    return;
                }
            }
        }
        private List<Field> GetFieldsValue(List<Field> fields, FormCollection formCollection)
        {
            foreach (var field in fields)
            {
                field.Value = formCollection[field.Id];
            }
            return fields;
        }
        private List<EmailTemplate> GetEmailTemplates(IPublishedContent formNode)
        {
            var query = formNode.Descendants().Where(n => n.DocumentTypeAlias == NodeAlias.EmailTemplate).Select(e => GetEmailTemplate(e));
            return query != null ? query.ToList() : new List<EmailTemplate>();
        }
        private EmailTemplate GetEmailTemplate(IPublishedContent emailTemplateNode)
        {
            EmailTemplate emailTemplate = new EmailTemplate()
            {
                Id = emailTemplateNode.Id,
                SenderName = fieldService.PropertyAsString(emailTemplateNode, PropertyAlias.SenderName, string.Empty),
                From = fieldService.PropertyAsString(emailTemplateNode, PropertyAlias.From, string.Empty),
                To = fieldService.GetEmails(emailTemplateNode, PropertyAlias.To),
                Cc = fieldService.GetEmails(emailTemplateNode, PropertyAlias.Cc),
                Bcc = fieldService.GetEmails(emailTemplateNode, PropertyAlias.Bcc),
                Subject = fieldService.PropertyAsString(emailTemplateNode, PropertyAlias.Subject, string.Empty),
                Body = fieldService.PropertyAsString(emailTemplateNode, PropertyAlias.Body, string.Empty)
            };

            return emailTemplate;
        }
        private string GetFullMediaPath(int mediaId)
        {
            string mediaPath = string.Empty;
            DynamicMedia media = new DynamicMedia(mediaId);
            string mediaNiceUrl = media.NiceUrl;
            if (!string.IsNullOrEmpty(mediaNiceUrl))
            {
                dynamic mediaVal = null;
                try
                {
                    mediaVal = System.Web.Helpers.Json.Decode(mediaNiceUrl);
                    mediaNiceUrl = mediaVal.src;
                }
                catch (Exception ex)
                {
                    //When can't decode -> ignored
                    LogHelper.Error<string>("Can't decode the media while uploading", ex);
                }
            }
            mediaPath = string.Format("{0}{1}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), mediaNiceUrl);
            return mediaPath;
        }
        private List<EmailTemplate> UpdatingTemplate(List<EmailTemplate> emailTempaltes, List<Field> fields)
        {

            foreach (EmailTemplate emailTempalte in emailTempaltes)
            {
                var emailTo = ParsingEmailAddress(emailTempalte.To, fields);
                var emailCc = ParsingEmailAddress(emailTempalte.Cc, fields);
                var emailBcc = ParsingEmailAddress(emailTempalte.Bcc, fields);
            }
            return emailTempaltes;
        }
        private List<EmailAddress> ParsingEmailAddress(List<EmailAddress> emailAddresses, List<Field> fields)
        {
            foreach (EmailAddress emailAddress in emailAddresses)
            {
                if (emailAddress.Alias == PropertyAlias.EmailField)
                {
                    var fieldAsEmail = fields.Where(f => f.Id == emailAddress.Id && !string.IsNullOrEmpty(f.Value)).FirstOrDefault();
                    if (fieldAsEmail != null)
                    {
                        MailAddress newEmail = new MailAddress(fieldAsEmail.Value);
                        if (newEmail != null)
                        {
                            emailAddress.MailAddress = newEmail;
                        }
                    }
                }
            }
            emailAddresses = emailAddresses.Where(e => e.MailAddress != null).ToList();
            return emailAddresses;
        }
    }
}
