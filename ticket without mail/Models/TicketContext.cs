using System.Data.Entity;

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