using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class SiteModel
    {

        public List<Ticket> unresolvedTickets { get; set; }
        public List<resolvedTickets> resolvedTickets { get; set; }
        public List<string> emails { get; set; }
        public SiteModel()
        {
            unresolvedTickets = new List<Ticket>();
            resolvedTickets = new List<resolvedTickets>();
            emails = new List<string>();
        }
    }
}