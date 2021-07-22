using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ticket_without_mail.Models
{
    public class resolvedTickets
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Вашата e-mail адреса")]
        public string email { get; set; }
        [Required]
        [DisplayName("Наслов на проблемот")]
        public string problemSubject { get; set; }
        [Required]
        [DisplayName("Опис на проблемот")]
        public string problemBody { get; set; }

        public string ipv4 { get; set; }
        [DisplayName("Време на отварање")]
        public DateTime submitTime { get; set; }

        [DisplayName("Време на затваорање")]
        public DateTime resolveTime { get; set; }

        [DisplayName("Проблемот е решен од")]
        public string resolver { get; set; }

        [DisplayName("Потребно време")]
        public TimeSpan totalTime { get; set; }

        public resolvedTickets() { }
        public resolvedTickets(int id, string email, string problemSubject, string problemBody, DateTime submitTime, DateTime resolveTime, string resolver, string ipv4)
        {
            this.Id = id;
            this.email = email;
            this.problemSubject = problemSubject;
            this.problemBody = problemBody;
            this.submitTime = submitTime;
            this.resolveTime = resolveTime;
            this.resolver = resolver;
            this.ipv4 = ipv4;
        }
    }
}