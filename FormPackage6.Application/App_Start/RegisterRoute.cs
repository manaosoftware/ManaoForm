using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Web;

namespace FormPackage6.Application.App_Start
{
    public class RegisterRoute : IApplicationEventHandler
    {
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication,ApplicationContext applicationContext)
        { 
            RouteTable.Routes.MapRoute(
                "ManaoForm",
                "ManaoFormApi/{Action}",
                new
                {
                    Controller = "AjaxForm",
                    Action = "{Action}"
            });

        }

        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication,ApplicationContext applicationContext)
        {

        }
        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication,ApplicationContext applicationContext)
        {
         
        }
    }
}
