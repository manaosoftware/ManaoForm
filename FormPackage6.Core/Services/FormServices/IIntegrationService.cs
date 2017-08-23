using FormPackage6.Core.DomainModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.Services.FormServices
{
    public interface IIntegrationService
    {
        Message GetKeyFromDictionary(Message message);
    }
}
