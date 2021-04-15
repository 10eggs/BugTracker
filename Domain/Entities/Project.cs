using Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public ProjectOwner ProjectOwner { get; set; }
        public int ProjectOwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<RequestItem> Requests { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        public IEnumerable<QA> QAs { get; set; }
    }
}
