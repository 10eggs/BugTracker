using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.UserView
{
    public class PendingRequestModel : PageModel
    {
        private readonly IRequestPersistance _rp;

        public PendingRequestModel(IRequestPersistance rp)
        {
            _rp = rp;
        }
        public async Task OnGet()
        {

        }
        public async Task<JsonResult> OnGetPopulateTable()
        {
            var requests = await _rp.GetCreatedByAuthorAsync(User.Identity.Name);
            var data = RequestModelView.CreateModelView(requests);

            return new JsonResult(new { data });
        }

    }

    public class RequestModelView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        private static IEnumerable<RequestModelView> Transform(List<Request> requests)
        {
            foreach (var r in requests)
            {
                yield return new RequestModelView { Id = r.Id, Title = r.Title, Description = r.Description, Project = r.Project.Name };
            }
        }

        public static List<RequestModelView> CreateModelView(List<Request> requests)
        {
            return Transform(requests).ToList();
        }
    }
}
