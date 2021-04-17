using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Infrastructure.Persistance;
using Domain.Entities;

namespace BugTrackerWeb.Pages.Dashboard
{
    public class CreateTicketModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IRequestPersistance _rp;
        private readonly IProjectPersistance _pp;
        private readonly ITicketPersistance _tp;
        public CreateTicketModel(ApplicationDbContext context, IRequestPersistance rp, IProjectPersistance pp,ITicketPersistance tp)
        {
            _ctx = context;
            _rp = rp;
            _pp = pp;
            _tp = tp;
        }

        
        [BindProperty]
        public RequestItem NewRequest { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public List<SelectListItem> ProjectListDDL { get; set; }
        

        public void OnGet()
        {
            ProjectListDDL = PopulateProjectList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                AssignUserToRequest();
                await _rp.SaveAsync(NewRequest);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public void AssignUserToRequest()
        {
            NewRequest.Author = User.Identity.Name;
        }

        //Maybe it could be generic at some point?
        public List<SelectListItem> PopulateProjectList()
        {

            return _pp.GetAll().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name

            }).ToList();
        }
    }
}

//using (var db = _ctx)
//{
//    var tp = new TicketPersistance(_ctx);
//    AssignUserId();
//    await tp.Save(NewTicket);
//    return RedirectToPage("Index");
//}