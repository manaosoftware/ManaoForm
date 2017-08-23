using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using FormPackage6.Infrastructure.FormServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace FormPackage6.Application.ApiControllers
{
    [PluginController("ManaoForm")]
    public class FormPluginController : UmbracoAuthorizedJsonController
    {
        private IFormPluginService formPluginService;
        private IFormService formService;
        public FormPluginController(IFormPluginService formPluginService, IFormService formService)
        {
            this.formPluginService = formPluginService;
            this.formService = formService;
        }
        public List<Field> GetFormFields(int Id, string fieldAlias)
        {
            return formPluginService.GetFields(Id, fieldAlias);
        }
        public List<Form> GetForms()
        {
            return formService.GetForms();
        }
    }
}
