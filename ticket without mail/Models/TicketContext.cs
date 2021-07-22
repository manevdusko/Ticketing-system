using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class TicketContext: DbContext
    {
        public DbSet<resolvedTickets> resolvedTickets { get; set; }

        public TicketContext() : base("DefaultConnection") { }
        public static TicketContext Create()
        {
            return new TicketContext();
        }

        public System.Data.Entity.DbSet<ticket_without_mail.Models.Ticket> Tickets { get; set; }

    }
}