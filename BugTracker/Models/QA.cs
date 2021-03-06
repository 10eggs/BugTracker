using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Models
{
    public class QA
    {
        public QA()
        {
            Tickets = new List<Ticket>();
        }
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
