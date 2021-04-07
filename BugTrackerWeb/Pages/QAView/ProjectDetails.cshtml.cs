using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Models.TicketProperties;
using BugTracker.Persistance;
using BugTracker.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BugTrackerWeb.Pages.QAView
{
    public class ProjectDetailsModel : PageModel
    {
        private readonly IProjectPersistance _pp;
        private readonly IMemoryCache _cache;
        public ProjectDetailsModel(IProjectPersistance pp, IMemoryCache cache)
        {
            _pp = pp;
            _cache = cache;
        }

        [BindProperty]
        public List<Ticket> TicketList { get; set; }

        [BindProperty(SupportsGet = true)]
        public Project Project { get; set; }

        public ResponseModel EditedTicket { get; set; }

        public SelectList TicketStatDDLOptions { get; set; }
        public string Description { get; set; }


        public async Task OnGet(int projectId, int qaId)
        {
            TicketList = await _pp.GetRelatedTicketsAssignedToQa(projectId, qaId);

            var input = JsonConvert.SerializeObject(TicketModelView.CreateModelView(TicketList));

            //This should be done in separate method or even separate view
            TicketStatDDLOptions = new SelectList(EnumUtil.GetValues<TicketStatus>());

            _cache.Set("Tickets", input);

           
        }

        public async Task<JsonResult> OnGetShowDetails(int ticketId)
        {
            var rawTickets = _cache.Get("Tickets").ToString();
            List<TicketModelView> ticketModel = JsonConvert.DeserializeObject<List<TicketModelView>>(rawTickets);
            TicketModelView ticket = ticketModel.Where(t => t.Id == ticketId).FirstOrDefault();
            return new JsonResult(ticket);
        }


        public async Task<JsonResult> OnPostUpdateTicket([Bind(Prefix =nameof(EditedTicket))] ResponseModel response)
        {
            var r = response;
            EditedTicket = response;
            if (!ModelState.IsValid)
            {
                 return new JsonResult(new { success = false, errormessage = "ModelState is invalid, try again" });
            }

             return new JsonResult(new { success = true, message = "Model state is valid here!" });
        }
    }
    public class ResponseModel
    {
        public TicketStatus Status { get; set; }

        [Required]
        public string Description { get; set; }
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
        public List<string> TicketStatusType { get {
                return ReturnDropDownListValues<TicketStatus>(EnumUtil.GetValues<TicketStatus>());
            }
        }

        public string TicketStatus { get; set; }
        public List<string> TickerPriorityType { 
            get {
                return ReturnDropDownListValues<TicketPriority>(EnumUtil.GetValues<TicketPriority>());
            }
        }
        public string TicketPriority { get; set; }

        public List<string> TicketCategoryType{ 
            get {
                return ReturnDropDownListValues<TicketCategory>(EnumUtil.GetValues<TicketCategory>());
            }
        }
        public string TicketCategory { get; set; }
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

                    TicketStatus = t.TicketStatus.ToString(),
                    TicketPriority = t.TicketPriority.ToString(),
                    TicketCategory = t.TicketCategory.ToString(),

                };
            }
        }

        public List<string> ReturnDropDownListValues<T>(IEnumerable<T> typeList)
        {

            List<string> categoryList = new List<string>();
            foreach(var t in typeList)
            {
                categoryList.Add(t.ToString());
            }
            return categoryList;

        }

        public static List<TicketModelView> CreateModelView(List<Ticket> tickets)
        {
            return Transform(tickets).ToList();
        }


    }

 
}

