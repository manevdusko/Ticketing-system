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

        [DisplayName("Време на затварање")]
        public DateTime resolveTime { get; set; }

        [DisplayName("Време на прифаќање")]
        public DateTime? acceptanceTime { get; set; }

        [DisplayName("Проблемот е решен од")]
        public string resolver { get; set; }

        [DisplayName("Забелешка")]
        public string note { get; set; }

        [DisplayName("Тип на проблем")]
        public List<String> problemType { get; set; }

        public int days { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }

        public resolvedTickets() 
        {
            problemType = new List<string>();
            acceptanceTime = DateTime.UtcNow;

        }
        public resolvedTickets(int id, string email, string problemSubject, string problemBody, DateTime submitTime, DateTime acceptanceTime, DateTime resolveTime, string resolver, string ipv4)
        {
            acceptanceTime = DateTime.UtcNow;
            problemType = new List<string>();
            this.Id = id;
            this.email = email;
            this.problemSubject = problemSubject;
            this.problemBody = problemBody;
            this.submitTime = submitTime;
            this.resolveTime = resolveTime;
            this.resolver = resolver;
            this.acceptanceTime = acceptanceTime;
            this.ipv4 = ipv4;
        }
    }
}