using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class ProblemType
    {
        [Key]
        public int id { get; set; }
        public string problemName { get; set; }
    }
}