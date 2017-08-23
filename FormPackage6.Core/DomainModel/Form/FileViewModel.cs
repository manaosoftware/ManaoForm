using System.Web;

namespace FormPackage6.Core.DomainModel.Form
{
    public class FileViewModel
    {
        public HttpPostedFileBase file { get; set; }
        public string name { get; set; }
    }
}
