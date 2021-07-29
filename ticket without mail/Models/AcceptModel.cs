using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class AcceptModel
    {
        public Ticket ticket { get; set; }
        public List<string> emails { get; set; }
    }
}