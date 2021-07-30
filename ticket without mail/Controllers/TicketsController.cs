using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Mvc;
using ticket_without_mail.Models;

namespace ticket_without_mail.Controllers
{
    public class TicketsController : Controller
    {
        private TicketContext db = new TicketContext();
        private static List<Ticket> selektirani = new List<Ticket>();
        private static List<resolvedTickets> resolvedSelektirani = new List<resolvedTickets>();

        [HttpPost]
        public FileResult ExportToCSVTicket()
        {/*Ticket Model
            * email = ticket.email
            * naslov = problemSubject
            * opis = problemBody
            * ip adresa = ipv4
            * tip na problem = problemType
            * vreme na otvaranje = submitTime
            * vreme na prifakjanje = acceptanceTime
            * koj go prifatil problemot = acceptor
            */
            StringBuilder sb = new StringBuilder();
            sb.Append("email,naslov,opis,vreme,ip adresa");
            sb.Append("\r\n");
            foreach (Ticket item in selektirani)
            {
                sb.Append(item.email + "," + item.problemSubject + "," + item.problemBody + "," + item.submitTime + "," + item.ipv4);
                sb.Append("\r\n");
            }

            return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "tiketi.csv");
        }

        [HttpPost]
        public FileResult ExportToCSVResolvedTicket()
        {
            /*resolvedTickets Model
         * email = ticket.email
         * naslov = problemSubject
         * opis = problemBody
         * ip adresa = ipv4
         * tip na problem = problemType
         * vreme na otvaranje = submitTime
         * vreme na prifakjanje = acceptanceTime
         * vreme na zatvaranje = resolveTime
         * days, hours, minutes = potrebno vreme (taboteno)
         * koj go prifatil problemot = acceptor
         * zabeleska = note
         */
            StringBuilder sb = new StringBuilder();
            sb.Append("email,naslov,opis,vreme na otvaranje,potrebno vreme,vreme na zatvaranje,prifateno od,ip,tip na problem");
            sb.Append("\r\n");
            foreach (resolvedTickets item in resolvedSelektirani)
            {
                sb.Append(item.email + "," + item.problemSubject + "," + item.problemBody + "," + item.submitTime + "," + item.days + "; "+item.hours+":"+item.minutes+":"+item.seconds + ","+item.resolveTime+"," + item.resolver + "," + item.ipv4 + "," + item.problemType);
                sb.Append("\r\n");
            }

            return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", "reseni tiketi.csv");
        }

        // GET: Tickets
        [Authorize]
        public ActionResult Index()
        {
            if (Request.Form["from"] != null && Request.Form["to"] != null && Request.Form["from"] != "" && Request.Form["to"] != "")
            {
                selektirani.Clear();
                DateTime from = DateTime.Parse(Request.Form["from"]);
                DateTime to = DateTime.Parse(Request.Form["to"]);
                List<Ticket> site = db.Tickets.ToList();
                
                foreach (Ticket ticket in site)
                {
                    if (ticket.submitTime >= from && ticket.submitTime <= to)
                    {
                        selektirani.Add(ticket);
                    }
                }
                return View(selektirani);
            }
            else
            {
                selektirani.Clear();
                selektirani = db.Tickets.ToList();
                return View(selektirani);
            }
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

        [Authorize]
        public ActionResult Performancecalc()
        {
            int brojNaPodneseniTiketi = 0;
            int brojNaReseniTiketi = 0;
            int days=0, hours=0, minutes=0, seconds=0;
            List<Resolver> resolvers = new List<Resolver>();

            if (Request.Form["from"] != null && Request.Form["to"] != null && Request.Form["from"] != "" && Request.Form["to"] != "")
            {
                DateTime from = DateTime.Parse(Request.Form["from"]);
                DateTime to = DateTime.Parse(Request.Form["to"]);

                foreach (resolvedTickets ticket in db.resolvedTickets.ToList())
                {
                    //  if (ticket.submitTime.Month == id)
                    if (ticket.submitTime >= from && ticket.submitTime <= to)
                    {
                        brojNaPodneseniTiketi++;
                    }
                    // if (ticket.resolveTime.Month == id)
                    if (ticket.resolveTime >= from && ticket.resolveTime <= to)
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
                    // if (ticket.submitTime.Month == id)
                    if (ticket.submitTime >= from && ticket.submitTime <= to)
                    {
                        brojNaPodneseniTiketi++;
                    }
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

            /*Ticket Model
             * email = ticket.email
             * naslov = problemSubject
             * opis = problemBody
             * ip adresa = ipv4
             * tip na problem = problemType
             * vreme na otvaranje = submitTime
             * vreme na prifakjanje = acceptanceTime
             * koj go prifatil problemot = acceptor
             */

            /*resolvedTickets Model
             * email = ticket.email
             * naslov = problemSubject
             * opis = problemBody
             * ip adresa = ipv4
             * tip na problem = problemType
             * vreme na otvaranje = submitTime
             * vreme na prifakjanje = acceptanceTime
             * vreme na zatvaranje = resolveTime
             * days, hours, minutes = potrebno vreme (taboteno)
             * koj go prifatil problemot = acceptor
             * zabeleska = note
             */
            Ticket ticket = db.Tickets.Find(id);
            resolvedTickets resolvedTickets = new resolvedTickets(ticket.Id, ticket.email, ticket.problemSubject, ticket.problemBody, ticket.ipv4, Request.Form["tip"], ticket.submitTime, DateTime.UtcNow, ticket.acceptor, Request.Form["qty"]);
            if(ticket.acceptanceTime != null)
            resolvedTickets.acceptanceTime = ticket.acceptanceTime;
            Debug.WriteLine(Request.Form["tip"]);

            int rabotniMinuti = 0;
            if (Int32.TryParse(Request.Form["raboteno"], out rabotniMinuti))
            {
                if (rabotniMinuti >= 1440)
                {
                    resolvedTickets.days = Int32.Parse(Request.Form["raboteno"]) / 1440;
                    rabotniMinuti = resolvedTickets.days % 1440;

                    resolvedTickets.hours = Int32.Parse(Request.Form["raboteno"]) / 60;
                    rabotniMinuti = resolvedTickets.days % 60;

                    resolvedTickets.minutes = rabotniMinuti;
                }

                else if (Int32.Parse(Request.Form["raboteno"]) >= 60)
                {
                    resolvedTickets.hours = Int32.Parse(Request.Form["raboteno"]) / 60;
                    rabotniMinuti = resolvedTickets.days % 60;

                    resolvedTickets.minutes = rabotniMinuti;
                }
                else
                {
                    resolvedTickets.minutes = rabotniMinuti;
                }
            }
            db.Tickets.Remove(ticket);
            db.resolvedTickets.Add(resolvedTickets);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            AcceptModel acceptModel = new AcceptModel();
            acceptModel.ticket = ticket;
            List<ApplicationUser> users = applicationDbContext.Users.ToList();
            List<string> userMails = new List<string>();

            foreach (ApplicationUser applicationUser in users)
            {
                userMails.Add(applicationUser.Email);
            }
            acceptModel.emails = userMails;
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(acceptModel);
        }

        // resolve ticket
        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            ticket.acceptanceTime = DateTime.UtcNow;
            //ticket.acceptor = User.Identity.Name;
            ticket.acceptor = Request.Form["res"];
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ResolvedTickets()
        {
            if (Request.Form["from"] != null && Request.Form["to"] != null && Request.Form["from"] != "" && Request.Form["to"] != "")
            {
                DateTime from = DateTime.Parse(Request.Form["from"]);
                DateTime to = DateTime.Parse(Request.Form["to"]);
                List<resolvedTickets> site = db.resolvedTickets.ToList();
                resolvedSelektirani = new List<resolvedTickets>();
                foreach (resolvedTickets ticket in site)
                {
                    if (ticket.submitTime >= from && ticket.submitTime <= to)
                    {
                        resolvedSelektirani.Add(ticket);
                    }
                }
                return View(resolvedSelektirani);
            }
            resolvedSelektirani = db.resolvedTickets.ToList();
            return View(resolvedSelektirani);
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