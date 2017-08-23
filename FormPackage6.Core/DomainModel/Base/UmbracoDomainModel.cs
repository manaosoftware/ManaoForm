using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace FormPackage6.Core.DomainModel.Base
{
    public abstract class UmbracoDomainModel : IRenderModel, IDomainModel
    {
        public int Id { get; set; }
        public CultureInfo CurrentCulture { get; set; }
        public IPublishedContent Content {get; set;}

    }
}
