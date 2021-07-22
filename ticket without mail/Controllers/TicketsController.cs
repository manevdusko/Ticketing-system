using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.Mvc;
using ticket_without_mail.Models;

namespace ticket_without_mail.Controllers
{
    public class TicketsController : Controller
    {
        private TicketContext db = new TicketContext();

        // GET: Tickets
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,email,problemSubject,problemBody")] Ticket ticket)
        {
            string ipv4 = "";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipv4 = ip.ToString();
                    }
                }
            }
            catch
            {
                Debug.WriteLine("No ip");
            }
            if (ModelState.IsValid)
            {
                ticket.ipv4 = ipv4;
                ticket.submitTime = DateTime.UtcNow;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("success");
            }

            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // resolve ticket
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            resolvedTickets resolvedTickets = new resolvedTickets(ticket.Id, ticket.email, ticket.problemSubject, ticket.problemBody, ticket.submitTime, DateTime.UtcNow, User.Identity.Name, ticket.ipv4);
            resolvedTickets.totalTime = resolvedTickets.resolveTime.Subtract(resolvedTickets.submitTime);
            db.Tickets.Remove(ticket);
            db.resolvedTickets.Add(resolvedTickets);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ResolvedTickets()
        {
            return View(db.resolvedTickets.ToList());
        }

        public ActionResult success()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}