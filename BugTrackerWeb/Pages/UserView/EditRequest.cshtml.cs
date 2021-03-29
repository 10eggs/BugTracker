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
    public class EditTicketModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private readonly IRequestPersistance _rp;
        private readonly ITicketPersistance _tp;
        private readonly IProjectPersistance _pp;
        public EditTicketModel(AppDbContext context, IRequestPersistance rp, ITicketPersistance tp, IProjectPersistance pp)
        {
            _ctx = context;
            _rp = rp;
            _tp = tp;
            _pp = pp;

        }

        [BindProperty]
        public Request EditedRequest { get; set; }

        [BindProperty]
        public List<SelectListItem> ProjectListDDL { get; set; }

        public async Task OnGet(int id)
        {
            EditedRequest = await _rp.GetByIdAsync(id);
            ProjectListDDL = PopulateProjectList();
        }

        public async Task<IActionResult> OnPost()
        {
            //hidden input for id
            //Read about tags helpers
            if (ModelState.IsValid)
            {
                var et = await _rp.GetByIdAsync(EditedRequest.Id);

                await _rp.Edit(et, EditedRequest.Title, EditedRequest.Description);
                await _ctx.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
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
