using FormPackage6.Core.DomainModel.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.ValidationHandler.Base
{
    public class CustomAbstractValidator<T> : AbstractValidator<T>, IValidationHandler<T> where T : IDomainModel
    {
        public ValidationResult Validate(T instance)
        {
            return new ValidationResult(base.Validate(instance));
        }
    }
}
