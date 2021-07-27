using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class AdditionalModel
    {
        public Ticket ticket { get; set; }
        public List<ProblemType> problemTypes { get; set; }
        public AdditionalModel()
        {
            problemTypes = new List<ProblemType>();
        }
    }
}