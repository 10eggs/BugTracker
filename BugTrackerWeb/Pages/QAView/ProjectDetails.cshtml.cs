using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Models.TicketProperties;
using BugTracker.Persistance;
using BugTracker.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BugTrackerWeb.Pages.QAView
{
    public class ProjectDetailsModel : PageModel
    {
        private readonly IProjectPersistance _pp;
        private readonly IMemoryCache _cache;
        public ProjectDetailsModel(IProjectPersistance pp,IMemoryCache cache)
        {
            _pp = pp;
            _cache = cache;
        }
        [BindProperty]
        public List<Ticket> TicketList { get; set; }

        [BindProperty(SupportsGet = true)]
        public Project Project { get; set; }

        private List<TicketModelView> TicketModelViewList { get; set; }

        public async Task OnGet(int projectId, int qaId)
        {
            TicketList = await _pp.GetRelatedTicketsAssignedToQa(projectId, qaId);

            var input = JsonConvert.SerializeObject(TicketModelView.CreateModelView(TicketList));

            _cache.Set("Tickets", input);

            //TicketModelViewList = TempData.Get<List<TicketModelView>>("ticketList");
           
        }

        public async Task<JsonResult> OnGetShowDetails(int ticketId)
        {
            var rawTickets = _cache.Get("Tickets").ToString();
            List<TicketModelView> ticketModel = JsonConvert.DeserializeObject<List<TicketModelView>>(rawTickets);
            TicketModelView ticket = ticketModel.Where(t => t.Id == ticketId).FirstOrDefault();
            var p = "p";
            return new JsonResult(ticket);
        }


    }
    public class TicketModelView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string RequestAuthor { get; set; }
        public DateTime Date { get; set; }
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        public int QaID { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public DateTime Updated { get; set; }
        private static IEnumerable<TicketModelView> Transform(List<Ticket> tickets)
        {
            foreach (var t in tickets)
            {
                yield return new TicketModelView { Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Author = t.Author,
                    RequestAuthor = t.RequestAuthor,
                    Date = t.Date,
                    ProjectName = t.Project.Name,
                    ProjectId = t.ProjectId,
                    QaID = t.QaID,
                    TicketStatus = t.TicketStatus,
                    TicketPriority = t.TicketPriority,
                    TicketCategory = t.TicketCategory
                     };
            }
        }

        public static List<TicketModelView> CreateModelView(List<Ticket> tickets)
        {
            return Transform(tickets).ToList();
        }


    }

 
}

