using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using FormPackage6.Core.Services.UmbracoServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web;

namespace FormPackage6.Application.ApiControllers
{
    public class AjaxFormController : SurfaceController
    {
        private IFormService formService;

        public AjaxFormController(IFormService formService)
        {
            this.formService = formService;
        }

        [HttpPost]
        public ActionResult AjaxSubmit(string data)
        {
            List<FileViewModel> uploadedFiles = new List<FileViewModel>();

            if (Request.Files.Count > 0)
            {
                uploadedFiles = Request.Files.AllKeys.Select(x => new FileViewModel { file = Request.Files[x], name = x }).Where(x => x.file.ContentLength > 0).ToList();
            }

            Result result = new Result();
            List<FieldData> fields = null;

            try
            {
                fields = JsonConvert.DeserializeObject<List<FieldData>>(data);
            }
            catch (Exception ex)
            {
                FieldError fieldError = new FieldError { FieldName = "global", ErrorType = "JSON form parse error" };
                result.Errors.Add(fieldError);
                result.Status = "fail";
                result.Message = ex.Message;
            }

            if (result.Status != "fail")
            {
                FormCollection formCollection = new FormCollection();
                foreach (var field in fields)
                {
                    formCollection.Add(field.Name, field.Value);
                }

                result = formService.Submit(formCollection, uploadedFiles);
                result.Message = !string.IsNullOrEmpty(result.Message) ? result.Message : result.Status;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
