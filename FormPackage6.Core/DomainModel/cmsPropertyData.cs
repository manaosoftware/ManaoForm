using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.DomainModel
{
    public class cmsPropertyData
    {
        public int id { get; set; }
        public int contentNodeId { get; set; }
        public Guid? versionId { get; set; }
        public int propertytypeid { get; set; }
        public int? dataInt { get; set; }
        public decimal? dataDecimal { get; set; }
        public DateTime? dataDate { get; set; }
        public string dataNvarchar { get; set; }
        public string dataNtext { get; set; }
    }
}
