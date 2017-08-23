using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace FormPackage6.Core.DomainModel.Form
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string From { get; set; }
        public List<EmailAddress> To { get; set; }
        public List<EmailAddress> Cc { get; set; }
        public List<EmailAddress> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class EmailAddress
    {
        public string Id { get; set; }
        public string Alias { get; set; }
        public MailAddress MailAddress { get; set; }
    }
}
