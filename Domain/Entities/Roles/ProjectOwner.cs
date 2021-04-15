using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Roles
{
    public class ProjectOwner
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}
