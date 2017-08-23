using FormPackage6.Core.DomainModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FormPackage6.Core.Services.FormServices
{
    public interface IFormService
    {
        List<Form> GetForms();
        Form GetByNodeId(int Id);
        void SaveUploadFiles(ref Form form, List<FileViewModel> uploadedFiles);
        Result Submit(FormCollection formCollection, List<FileViewModel> uploadedFiles);
        Result Validate(Form formModel, List<FileViewModel> uploadedFiles);
        void SendEmail(Form formModel);

    }
}
