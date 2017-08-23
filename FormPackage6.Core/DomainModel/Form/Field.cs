using System.Collections.Generic;

namespace FormPackage6.Core.DomainModel.Form
{
    public class Field
    {
        public string Id { get; set; }
        public FieldType FieldType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Placeholder { get; set; }
        public bool Mandatory { get; set; }
        public List<Option> Options { get; set; }
        public string Html { get; set; }
        public string SelectFileText { get; set; }
        public string ChangeFileText { get; set; }
        public MediaFolder MediaFolder { get; set; }
        public int MaxFileSize { get; set; }
        public bool IsRequiredValid { get; set; }
        public bool IsFormatValid { get; set; }
        public bool IsContainHTML { get; set; }
        public string MessageInvalidFormat { get; set; }
        public string MessageMandatory { get; set; }
        public string MessageOverMaxFileSize { get; set; }
    }
}
