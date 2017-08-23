using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormPackage6.Core.ValidationHandler.Base;
using FormPackage6.Core.DomainModel.Base;

namespace FormPackage6.Dispatcher
{
    public class DefaultValidateBus : IValidateBus
    {
        private IContainer ioCContainer;

        public DefaultValidateBus(IContainer ioCContainer)
        {
            this.ioCContainer = ioCContainer;
        }
        public ValidationResult Validate<TDomainModel>(TDomainModel validate) where TDomainModel : IDomainModel
        {
            IValidationHandler<TDomainModel> handler = null;
            try
            {
                handler = ioCContainer.GetInstance<IValidationHandler<TDomainModel>>();
                if (!((handler != null) && handler is IValidationHandler<TDomainModel>))
                    throw new Exception(string.Format("Validation handler not found for command type: {0}", typeof(TDomainModel)));

                return handler.Validate(validate);
            }
            catch (StructureMapConfigurationException ex)
            {
                return new ValidationResult();
                //throw new Exception(string.Format("Validation handler not found for command type: {0}", typeof(TDomainModel)));
            }
        }
    }
}
