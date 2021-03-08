using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Models
{
    public class Project
    {
        public Project()
        {
            Tickets = new List<Ticket>();
        }

        public Project(List<Ticket> tickets)
        {
            Tickets = tickets;
        }

        [Key]
        public int Id { get; set; }
        //Navigation property
        public ProjectOwner ProjectOwner { get; set; }
        public string ProjectOwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
