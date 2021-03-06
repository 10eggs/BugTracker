using BugTracker.Models.TicketProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Ticket
    {
        public Ticket(string title, string description, string author)
        {
            Title = title ?? throw new ArgumentNullException(paramName: nameof(title));
            Description = description ?? throw new ArgumentNullException(paramName: nameof(description));
            Author = author ?? throw new ArgumentNullException(paramName: nameof(author));
            Date = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm"), "yyyy-MM-dd HH:mm", null);
        }

        public Ticket(string title, string description, string author, Project project):this(title,description,author)
        {
            Project = project;
        }

        public Ticket()
        {

        }
        [Key]
        public int Id { get; set; }
        //[Required]
        public string Title { get; set; }
        //[Required]
        public string Description { get; set; }
        public string Author { get; set; }
        public string RequestAuthor { get; set; }
        public DateTime Date { get; set; }
        //Navigation property
        public Project Project { get; set; }
        //ForeignKey
        //[Required]
        public int ProjectId { get; set; }
        public bool IsAssigned { get; set; }
        public QA Qa { get; set; }
        //[Required]
        public int QaID { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public DateTime Updated { get; set; }

    }
}
