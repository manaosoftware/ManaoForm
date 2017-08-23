using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using FormPackage6.Core.Services.UmbracoServices;

namespace FormPackage6.Infrastructure.FormServices
{
    public class IntegrationService : IIntegrationService
    {
        private IUmbracoService umbracoService;
        public IntegrationService(IUmbracoService _umbracoService)
        {
            umbracoService = _umbracoService;
        }
        public Message GetKeyFromDictionary(Message message)
        {
            string output = string.Empty;
            if (message != null)
            {
                output = umbracoService.GetDictionaryItem(message.Key, message.Description);
                message.Description = output;
            }
            return message;
        }
    }
}
