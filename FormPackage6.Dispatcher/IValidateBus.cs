using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormPackage6.Core.ValidationHandler.Base;
using FormPackage6.Core.DomainModel.Base;

namespace FormPackage6.Dispatcher
{
    public interface IValidateBus
    {
        ValidationResult Validate<TDomainModel>(TDomainModel domainModel) where TDomainModel : IDomainModel;
    }
}
