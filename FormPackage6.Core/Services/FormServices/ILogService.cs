using FormPackage6.Core.DomainModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace FormPackage6.Core.Services.FormServices
{
    public interface ILogService
    {

        string GetLogName(Form form, IContent formNode);
        Field GetLogField(string fieldValue);
        string GetLogDetail(Form form);
        bool CreateLog(Form formModel);
    }
}
