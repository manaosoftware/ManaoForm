using FormPackage6.Core.Alias;
using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using Skybrud.Umbraco.GridData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace FormPackage6.Infrastructure.FormServices
{
    public class FormPluginService: IFormPluginService
    {
        private UmbracoHelper umbracoHelper;
        private IFieldService fieldService;
        private IContentService contentService;
        public FormPluginService(
            IFieldService fieldService, 
            IContentService contentService,
            UmbracoHelper umbracoHelper)
        {

            this.fieldService = fieldService;
            this.contentService = contentService;
            this.umbracoHelper = umbracoHelper;
        }
        public List<Field> GetFields(int Id, string fieldAlias)
        {
            List<Field> fields = new List<Field>();
            var currentNode = contentService.GetById(Id);
            if (currentNode != null)
            {
                var currentForm = currentNode.ContentType.Alias== NodeAlias.FormNode ? currentNode:
                    contentService.GetAncestors(currentNode).Where(x => x.ContentType.Alias == NodeAlias.FormNode).FirstOrDefault();
                
                if (currentForm != null)
                {
                    GridDataModel formGrid = GridDataModel.Deserialize(currentForm.GetValue(PropertyAlias.FormGrid).ToString());
                    if (formGrid != null)
                    {
                        fieldAlias = String.IsNullOrEmpty(fieldAlias) ? string.Empty : fieldAlias;
                        var filterField = !String.IsNullOrWhiteSpace(fieldAlias) ? fieldAlias.Split(',') : null;
                        var allFields = fieldService.GridControlsAsFields(formGrid.GetAllControls());
                        var filedControls = filterField != null ? allFields.Where(f => filterField.Contains(f.FieldType.Type, StringComparer.OrdinalIgnoreCase)) : allFields;
                        fields = filedControls.ToList();
                    }
                }
            }
            return fields;
        }
    }
}
