using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Roles
{
    public class QA
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
