using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.DomainModel.Form
{
    public class FieldError
    {
        public string FieldId { get; set; }
        public string FieldName { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMessageMandatory { get; set; }
        public string ErrorMessageValidation { get; set; }
    }
}
