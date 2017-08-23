using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.ValidationHandler.Base
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<ValidationFailure>();
        }

        public ValidationResult(IList<ValidationFailure> errors) : this()
        {
            Errors = errors;
        }

        public ValidationResult(FluentValidation.Results.ValidationResult result) : this()
        {
            foreach (FluentValidation.Results.ValidationFailure failure in result.Errors)
            {
                Errors.Add(new ValidationFailure()
                {
                    AttemptedValue = failure.AttemptedValue,
                    ErrorMessage = failure.ErrorMessage,
                    PropertyName = failure.PropertyName
                });
            }
        }

        public IList<ValidationFailure> Errors { get; set; }
        public bool IsValid
        {
            get
            {
                if (Errors.Count() > 0)
                    return false;
                else
                    return true;
            }
        }
    }
}
