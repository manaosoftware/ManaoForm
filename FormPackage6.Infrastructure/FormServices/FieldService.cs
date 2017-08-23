using Archetype.Models;
using FormPackage6.Core.Alias;
using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using Lecoati.LeBlender.Extension.Models;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace FormPackage6.Infrastructure.FormServices
{
    public class FieldService: IFieldService
    {
        public List<Field> GridControlsAsFields(GridControl[] gridControls)
        {
            List<Field> fields = new List<Field>();
            foreach (var control in gridControls)
            {
                Field field = new Field();
                var value = control.JObject.GetValue("value").FirstOrDefault();
                if (value != null)
                {
                    field = LeBlenderValueAsField(control.JObject.GetValue("value").FirstOrDefault().ToString());
                    field.Id = (string)control.JObject.GetValue("guid");
                    field.FieldType = new FieldType() { Icon = control.Editor.Icon, Type = control.Editor.Alias };
                }

                if (!string.IsNullOrEmpty(field.Id))
                {
                    fields.Add(field);
                }
            }
            return fields;
        }
        public Field LeBlenderValueAsField(string jsonValue)
        {
            Field field = new Field();
            LeBlenderValue leBlenderValue = JsonConvert.DeserializeObject<LeBlenderValue>(jsonValue);
            field.Name = GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.Name);
            field.Placeholder = GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.Placeholder);
            field.Mandatory = GetValueAsBoolean(GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.Mandatory));
            field.MessageMandatory = GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.MessageMandatory);
            field.MessageInvalidFormat = GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.MessageInvalidFormat);
            field.MessageOverMaxFileSize = GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.MessageOverMaxFileSize); 
            field.Options = GetArchetypePrevalues(leBlenderValue, PropertyAlias.Prevalues);
            field.SelectFileText = GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.SelectFileText);
            field.ChangeFileText = GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.ChangeFileText);
            field.MaxFileSize = GetMaxRequestLength();
            field.MediaFolder = PropertyValueAsMediaFolder(GetLeBlenderValueFieldValue(leBlenderValue, PropertyAlias.MediaFolder));
            return field;
        }
        public List<Option> GetPrevalues(string csvValues)
        {
            var values = csvValues.Split(',').Select(o => new Option() { Name = o });
            return values.ToList();
        }
        public List<Option> GetArchetypePrevalues(LeBlenderValue leBlenderValue, string alias)
        {
            List<Option> options = new List<Option>();
            ArchetypeModel archetypeModel;
            try
            {
                archetypeModel = leBlenderValue.GetValue<ArchetypeModel>(alias);
                if (archetypeModel != null)
                {
                    var query = archetypeModel.Fieldsets.Where(f => !f.Disabled).Select(f => new Option() { Name = f.GetValue("value") });
                    if (query.Any())
                    {
                        options = query.ToList();
                    }
                }
                return options;
            }
            catch (Exception)
            {
                return options;
            }

        }
        public bool GetValueAsBoolean(string value)
        {
            bool output = false;
            if (!String.IsNullOrEmpty(value))
            {
                if (value == "1" || value.ToLower() == "true")
                {
                    output = true;
                }
            }
            else
            {
                output = false;
            }
            return output;
        }
        public string GetLeBlenderValueFieldValue(LeBlenderValue value, string alias)
        {
            if (value.HasProperty(alias))
            {
                return value.GetRawValue(alias);
            }
            return string.Empty;
        }
        public Field PropertyValueAsField(string jsonValue)
        {
            Field propertyValueAsField = JsonConvert.DeserializeObject<Field>(jsonValue);
            return propertyValueAsField;
        }
        public List<Field> PropertyValueAsFields(string arrayJsonValue)
        {
            List<Field> propertyValueAsField = JsonConvert.DeserializeObject<List<Field>>(arrayJsonValue);
            return propertyValueAsField;
        }
        public MediaFolder PropertyValueAsMediaFolder(string jsonValue)
        {
            MediaFolder mediaFolder = JsonConvert.DeserializeObject<MediaFolder>(jsonValue);
            return mediaFolder;
        }
        public bool IsValidEmailAddress(string emailAddress)
        {
            Regex emailPattern = new Regex("^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,253}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,253}[a-zA-Z0-9])?)*$");
            return emailPattern.IsMatch(emailAddress);
        }
        public bool IsValidTelephoneNumber(string telephoneNumber)
        {
            bool isContainNumber = telephoneNumber.ToString().Any(c => char.IsDigit(c));

            return isContainNumber;
        }
        public bool IsContainHTML(string text)
        {
            return text.Contains(">") || text.Contains("<") ? true : false;
        }
        public List<Field> GetEmailField(IPublishedContent emailTemplate, string archeTypeFieldAlias)
        {
            List<Field> emailFields = new List<Field>();
            ArchetypeModel archetypeModel;
            try
            {

                archetypeModel = emailTemplate.GetPropertyValue<ArchetypeModel>(archeTypeFieldAlias);
                foreach (ArchetypeFieldsetModel field in archetypeModel.Fieldsets.Where(x =>!x.Disabled))
                {

                    if (field.Alias == PropertyAlias.EmailAddress)
                    {
                        string value = field.GetValue(PropertyAlias.EmailAddress);
                        Field email = new Field() { FieldType = new FieldType() { Type = "MailAddress" }, Value = value };

                        if (email != null)
                        {
                            emailFields.Add(email);
                        }
                    }
                    else if (field.Alias == PropertyAlias.EmailField)
                    {
                        string value = field.GetValue(PropertyAlias.EmailField);
                        var propertyAsFields = PropertyValueAsFields(value);
                        foreach (var f in propertyAsFields)
                        {
                            Field email = new Field() { FieldType = new FieldType() { Type = "MailField" }, Value = f.Id };
                        }
                    }
                }
                return emailFields;
            }
            catch (Exception)
            {
                return emailFields;
            }
        }
        public List<EmailAddress> GetEmails(IPublishedContent emailTemplate, string archeTypeFieldAlias)
        {
            List<EmailAddress> emails = new List<EmailAddress>();

            ArchetypeModel archetypeModel;
            try
            {

                archetypeModel = emailTemplate.GetPropertyValue<ArchetypeModel>(archeTypeFieldAlias);
                foreach (ArchetypeFieldsetModel field in archetypeModel.Fieldsets.Where(x => !x.Disabled))
                {
                    MailAddress email;

                    if (field.Alias == PropertyAlias.EmailAddress)
                    {
                        string value = field.GetValue(PropertyAlias.EmailAddress);
                        email = new MailAddress(value);
                        if (email != null)
                        {
                            emails.Add(new EmailAddress() { Alias = PropertyAlias.EmailAddress, MailAddress = email });
                        }
                    }
                    else if (field.Alias == PropertyAlias.EmailField)
                    {
                        string value = field.GetValue(PropertyAlias.EmailField);
                        var propertyAsField = PropertyValueAsFields(value);
                        foreach (var e in propertyAsField)
                        {
                            emails.Add(new EmailAddress() { Id = e.Id, Alias = PropertyAlias.EmailField, MailAddress = null });
                        }
                    }
                }
                return emails;
            }
            catch (Exception)
            {
                return emails;
            }
        }
        public string PropertyAsString(IPublishedContent content, string alias, string defaultValue)
        {
            if (content.HasValue(alias))
            {
                return content.GetPropertyValue<string>(alias);
            }
            else
            {

                return defaultValue;
            }
        }
        private int GetMaxRequestLength()
        {
            int maxRequestLength = 0;
            HttpRuntimeSection section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;

            if (section != null)
                maxRequestLength = section.MaxRequestLength;

            return maxRequestLength;
        }
    }
}
