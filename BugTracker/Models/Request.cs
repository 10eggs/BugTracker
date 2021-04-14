using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Models
{
    public class Request
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public Project Project { get; set; }
        [Required]
        public int ProjectId { get; set; }

        public Request()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
