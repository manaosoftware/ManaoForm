using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.DomainModel.Form
{
    public class Result
    {
        public string Status { get; set; }
        public List<FieldError> Errors { get; set; }
        public string Message { get; set; }
    }
}
