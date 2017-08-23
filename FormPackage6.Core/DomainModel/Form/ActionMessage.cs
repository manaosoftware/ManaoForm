using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.DomainModel.Form
{
    public class ActionMessage
    {
        public bool IsSuccess { get; set; }
        public Message Message { get; set; }
    }
    public class Message
    {
        public string Key { get; set; }
        public string Description { get; set; }
    }
}
