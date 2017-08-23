using FormPackage6.Core.ValidationHandler.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.Services
{
    public class ServiceResult
    {
        public ValidationResult ValidationResult { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsSuccess {
            get
            {
                if (!ValidationResult.IsValid || !String.IsNullOrEmpty(ErrorMessage))
                    return false;
                else
                    return true;
            }
        }
        public ServiceResult()
        {
            ValidationResult = new ValidationResult();
        }

        public ServiceResult(ValidationResult validateResult)
        {
            ValidationResult = validateResult;
        }

    }
}
