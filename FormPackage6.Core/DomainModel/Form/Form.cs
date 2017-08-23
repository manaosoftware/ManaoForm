using Skybrud.Umbraco.GridData;
using System.Collections.Generic;

namespace FormPackage6.Core.DomainModel.Form
{
    public class Form
    {
        public bool IsSaveLog { get; set; }
        public Field LogName { get; set; }
        public string PostUrl { get; set; }
        public string IPAddress { get; set; }
        public string SuccessPageUrl { get; set; }
        public int Id { get; set; }
        public string FormId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<Field> Fields { get; set; }
        public GridDataModel DataModel { get; set; }
        public string ButtonText { get; set; }
        public bool IsEnableSpamFilter { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<EmailTemplate> Templates { get; set; }
    }
}
