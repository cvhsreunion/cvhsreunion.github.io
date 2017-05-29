using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CVHSReunion.Models
{
    public class History
    {
        [Key]
        public int historyId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public string status { get; set; }
        public string helpPlanning { get; set; }
    }
}