using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Models
{
    public class ProjectOwner
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<Project> Projects { get; set; }
    }
}
