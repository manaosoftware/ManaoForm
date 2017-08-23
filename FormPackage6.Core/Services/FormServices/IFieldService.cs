using FormPackage6.Core.DomainModel.Form;
using Lecoati.LeBlender.Extension.Models;
using Skybrud.Umbraco.GridData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace FormPackage6.Core.Services.FormServices
{
    public interface IFieldService
    {
        List<Field> GridControlsAsFields(GridControl[] gridControls);
        Field LeBlenderValueAsField(string jsonValue);
        List<Option> GetPrevalues(string csvValues);
        List<Option> GetArchetypePrevalues(LeBlenderValue leBlenderValue, string alias);
        bool GetValueAsBoolean(string value);
        string GetLeBlenderValueFieldValue(LeBlenderValue value, string alias);
        Field PropertyValueAsField(string jsonValue);
        List<Field> PropertyValueAsFields(string arrayJsonValue);
        MediaFolder PropertyValueAsMediaFolder(string jsonValue);
        bool IsValidEmailAddress(string emailAddress);
        bool IsValidTelephoneNumber(string telephoneNumber);
        bool IsContainHTML(string text);
        List<Field> GetEmailField(IPublishedContent emailTemplate, string archeTypeFieldAlias);
        List<EmailAddress> GetEmails(IPublishedContent emailTemplate, string archeTypeFieldAlias);
        string PropertyAsString(IPublishedContent content, string alias, string defaultValue);
    }
}
