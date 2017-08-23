using FormPackage6.Core.Alias;
using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace FormPackage6.Infrastructure.FormServices
{
    public class LogService : ILogService
    {
        private IContentService contentService;
        public LogService(IContentService contentService)
        {
            this.contentService = contentService;
        }
        public string GetLogName(Form form, IContent formNode)
        {
            Field fieldModel = this.GetLogField(formNode.GetValue<string>(PropertyAlias.LogName));

            Field logField = form.Fields.Where(f => f.Id.Equals(fieldModel.Id)).FirstOrDefault();

            string logName = string.Empty;
            if (logField != null)
            {
                logName = logField.Value;
            }
            else if (form.Fields.Count > 0)
            {
                logName = form.Fields[0].Value;
            }
            return logName;
        }
        public Field GetLogField(string fieldValue)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var fields = serializer.Deserialize<List<Field>>(fieldValue);
            return fields.Count > 0 ? fields[0] : null;
        }

        public string GetLogDetail(Form form)
        {
            StringBuilder formLog = new StringBuilder();
            foreach (Field field in form.Fields)
            {
                string value = string.Empty;
                if (!string.IsNullOrEmpty(field.Value))
                    value = field.Value.Replace(Environment.NewLine, "<br />");

                formLog.AppendLine(string.Format("<p>{0}: {1}</p>", field.Name, value));
            }
            formLog.AppendLine(string.Format("<p>PageUrl: {0}</p>", form.PostUrl));
            formLog.AppendLine(string.Format("<p>Submitter IP: {0}</p>", form.IPAddress));
            formLog.AppendLine(string.Format("<p>Submit on: {0}</p>", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            return formLog.ToString();
        }
        public bool CreateLog(Form formModel)
        {
            bool result = false;
            if (formModel.IsSaveLog)
            {
                var formContent = contentService.GetById(formModel.Id);
                var logCollection = formContent.Children().Where(c => c.ContentType.Alias == NodeAlias.LogCollection).FirstOrDefault();
                var logName = DateTime.Now.ToString("yyyy-MM-dd - hh:mm:ss tt");
                if (formModel.LogName != null)
                {
                    var logNameQuery = formModel.Fields.Where(f => f.Id == formModel.LogName.Id).FirstOrDefault();
                    if (logNameQuery != null)
                    {
                        if (!string.IsNullOrEmpty(logNameQuery.Value))
                        {
                            logName = string.Format("{0} [{1}]", logName, logNameQuery.Value);
                        }
                    }
                }
                if (logCollection != null)
                {
                    IContent logNode = contentService.CreateContent(logName, logCollection, NodeAlias.Log, 0);

                    if (logNode.HasProperty(PropertyAlias.LogDetail))
                    {
                        string logDetail = GetLogDetail(formModel);
                        logNode.SetValue(PropertyAlias.LogDetail, logDetail);
                    }
                    result = contentService.SaveAndPublishWithStatus(logNode);
                }
            }
            return result;
        }
    }
}
