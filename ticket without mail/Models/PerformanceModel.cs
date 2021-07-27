using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class PerformanceModel
    {
        public int brojNaPodneseniTiketi { get; set; }
        public int brojNaReseniTiketi { get; set; }
        public float efikasnostVoProcenti { get; set; }
        public int days { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
        public List<Resolver> resolvers { get; set; } = new List<Resolver>();
    }
}