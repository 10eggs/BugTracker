using System;
using System.Collections.Generic;
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

        //Navigation property
        public ProjectOwner ProjectOwner { get; set; }
        public int ProjectOwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
