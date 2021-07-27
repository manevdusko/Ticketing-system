using System;
using System.Collections.Generic;
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

        public ActionResult AddSelection(string id)
        {
            ProblemType newProblem = new ProblemType();
            newProblem.problemName = id;
            db.ProblemTypes.Add(newProblem);
            Debug.WriteLine(id);
            Debug.WriteLine("OK");
            db.SaveChanges();
            return RedirectToAction("Index");
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

        public ActionResult Performance()
        {
            return View(db.resolvedTickets.ToList());
        }

        public ActionResult Performancecalc(int id)
        {
            int brojNaPodneseniTiketi = 0;
            int brojNaReseniTiketi = 0;
            int days=0, hours=0, minutes=0, seconds=0;
            List<Resolver> resolvers = new List<Resolver>();

            foreach (resolvedTickets ticket in db.resolvedTickets.ToList())
            {
                if (ticket.submitTime.Month == id)
                {
                    brojNaPodneseniTiketi++;
                }
                if (ticket.resolveTime.Month == id)
                {
                    days += ticket.days;
                    hours += ticket.hours;
                    minutes += ticket.minutes;
                    seconds += ticket.seconds;
                    if (resolvers.Find(x => x.ime.Equals(ticket.resolver)) != null)
                    {
                        Resolver resolver = resolvers.Find(x => x.ime.Equals(ticket.resolver));
                        resolvers.Remove(resolver);

                        resolver.brojNaReseniTiketi++;

                        resolver.days += ticket.days;
                        resolver.hours += ticket.hours;
                        resolver.minutes += ticket.minutes;
                        resolver.seconds += ticket.seconds;


                        resolvers.Add(resolver);

                    }
                    else
                    {
                        Resolver resolver = new Resolver();
                        resolver.brojNaReseniTiketi = 1;
                        resolver.ime = ticket.resolver;
                        resolver.days += ticket.days;
                        resolver.hours += ticket.hours;
                        resolver.minutes += ticket.minutes;
                        resolver.seconds += ticket.seconds;
                        resolvers.Add(resolver);
                    }
                    brojNaReseniTiketi++;
                }
            }
            foreach (Ticket ticket in db.Tickets.ToList())
            {
                if (ticket.submitTime.Month == id)
                {
                    brojNaPodneseniTiketi++;
                }
            }
            PerformanceModel performanceModel = new PerformanceModel();
            performanceModel.brojNaPodneseniTiketi = brojNaPodneseniTiketi;
            performanceModel.brojNaReseniTiketi = brojNaReseniTiketi;
            performanceModel.efikasnostVoProcenti = (float)brojNaReseniTiketi / brojNaPodneseniTiketi * (float)100.0;
            performanceModel.days = days;
            performanceModel.hours = hours;
            performanceModel.minutes = minutes;
            performanceModel.seconds = seconds;
            performanceModel.resolvers = resolvers;

            return View(performanceModel);
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
                ticket.submitTime = (DateTime)DateTime.UtcNow;
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
            AdditionalModel additionalModel = new AdditionalModel();
            additionalModel.ticket = db.Tickets.Find(id);
            additionalModel.problemTypes = db.ProblemTypes.ToList();
            //Ticket ticket = db.Tickets.Find(id);
            if (additionalModel.ticket == null)
            {
                return HttpNotFound();
            }
            return View(additionalModel);
        }



        // resolve ticket
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            resolvedTickets resolvedTickets = new resolvedTickets(ticket.Id, ticket.email, ticket.problemSubject, ticket.problemBody, ticket.submitTime, (DateTime)ticket.acceptanceTime, DateTime.UtcNow, User.Identity.Name, ticket.ipv4);
            resolvedTickets.acceptanceTime = ticket.acceptanceTime;
           
            DateTime at = (DateTime)resolvedTickets.acceptanceTime;

            resolvedTickets.days = DateTime.UtcNow.Subtract(at).Days;

            resolvedTickets.minutes = DateTime.UtcNow.Subtract(at).Minutes;

            resolvedTickets.hours = DateTime.UtcNow.Subtract(at).Hours;

            resolvedTickets.seconds = DateTime.UtcNow.Subtract(at).Seconds;
            resolvedTickets.note = Request.Form["qty"];
            db.Tickets.Remove(ticket);
            db.resolvedTickets.Add(resolvedTickets);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Accept(int? id)
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
        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            ticket.acceptanceTime = DateTime.UtcNow;
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