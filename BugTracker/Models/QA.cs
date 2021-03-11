using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Models
{
    public class QA
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
        public ICollection<AssignedTicket> Tickets { get; set; }

    }
}
