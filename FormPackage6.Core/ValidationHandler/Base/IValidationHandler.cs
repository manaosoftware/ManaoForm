using FormPackage6.Core.DomainModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.ValidationHandler.Base
{
    public interface IValidationHandler<in T> where T : IDomainModel
    {
        ValidationResult Validate(T validate);
    }
}
