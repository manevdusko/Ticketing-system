using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class Resolver
    {
        public int brojNaReseniTiketi { get; set; }
        public string ime { get; set; }
        public int days { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
    }
}