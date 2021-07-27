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
        public DbSet<ProblemType> ProblemTypes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}