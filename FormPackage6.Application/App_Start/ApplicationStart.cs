using AutoMapper;
using FormPackage6.Application.Alias;
using FormPackage6.Application.IoC;
using FormPackage6.Core.DomainModel;
using FormPackage6.Core.DomainModel.Base;
using FormPackage6.Core.Extensions;
using FormPackage6.Dispatcher;
using Lecoati.LeBlender.Extension.Models;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Skybrud.Umbraco.GridData;
using StructureMap;
using StructureMap.Web.Pipeline;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;
using System.Web.Optimization;

namespace FormPackage6.Application.App_Start
{
    public class ApplicationStart : ApplicationEventHandler
    {
        private IValidateBus validateBus;
        private IContainer container;
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
      {
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Setting Cinfiguration
            this.container = StructureMapContainerInit.InitializeContainer();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope);
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
            ////NEED FIX HERE


            AutoMapperConfiguration.Configure(this.container);

            validateBus = container.GetInstance<IValidateBus>();

            ////Listen for when content is being saved
            ContentService.Publishing += ContentService_Publishing;

            
            //TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;

        }

        private void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            var path = "/App_Plugins/UploadContent/UploadContent.html";
            var title = "Upload Content to Remote Server";

            var menu = new MenuItem();
            menu.Icon = "wine-glass";
            menu.SeperatorBefore = true;

            menu.LaunchDialogView(path, title);

            e.Menu.Items.Add(menu);
        }

        private void ContentService_Publishing(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            var domains = container.GetAllInstances<IDomainModel>();

            foreach (var entity in e.PublishedEntities)
            {
                #region validate model in grid 
                if (entity.HasProperty(PropertyAlias.GridContent))
                {
                    var json = entity.GetValue<string>(PropertyAlias.GridContent);

                    GridDataModel grid = GridDataModel.Deserialize(json);

                    foreach (var domain in domains)
                    {
                        var gridModels = grid.GetAllControls(domain.GetAliasName());
                        if (gridModels != null && gridModels.Any())
                        {
                            foreach (var gridModel in gridModels)
                            {
                                LeBlenderModel editor = Newtonsoft.Json.JsonConvert.DeserializeObject<LeBlenderModel>(gridModel.JObject.ToString());
                                
                                foreach (var item in editor.Items)
                                {
                                    var model = Mapper.Map(item, domain);
                                    ValidateModel(e, model);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Validate model
                else
                {
                    var domain = domains.FirstOrDefault(x => x.IsAliasOf(entity.ContentType.Alias));
                    if (domain != null)
                    {
                        var model = Mapper.Map(entity, domain);
                        ValidateModel(e, model);
                    }
                }
                #endregion
            }
        }

        private void ValidateModel(PublishEventArgs<IContent> e, IDomainModel model)
        {
            dynamic castModel = Convert.ChangeType(model, model.GetType());
            var result = validateBus.Validate(castModel);

            if (!result.IsValid)
            {
                e.Cancel = true;

                foreach (var error in result.Errors)
                {
                    var errors = new EventMessage(error.PropertyName, error.ErrorMessage, EventMessageType.Error);
                    e.CancelOperation(errors);
                }
            }
        }
    }
}
