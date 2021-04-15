using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerWeb.Pages.UserView
{
    public class NewRequestModel : PageModel
    {
        private readonly IRequestPersistance _rp;
        private readonly IProjectPersistance _pp;
        public NewRequestModel(AppDbContext context, IRequestPersistance rp, IProjectPersistance pp)
        {
            _rp = rp;
            _pp = pp;
        }


        [BindProperty]
        public Request NewRequest { get; set; }

        [BindProperty]
        public List<SelectListItem> ProjectListDDL { get; set; }

        public async Task OnGet()
        {
            ProjectListDDL = PopulateProjectList();

        }
        public async Task<IActionResult> OnPostSaveRequest()
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
