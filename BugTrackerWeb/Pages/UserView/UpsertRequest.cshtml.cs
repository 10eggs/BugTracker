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

namespace BugTrackerWeb.Pages.UserView
{
    public class UpsertModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private readonly IRequestPersistance _rp;
        public UpsertModel(AppDbContext context, IRequestPersistance rp)
        {
            _ctx = context;
            _rp = rp;
        }

        [BindProperty]
        public Request EditedRequest { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            EditedRequest = new Request();
            if (id == null)
            {
                return Page();
            }

            EditedRequest = await _rp.GetByIdAsync(id);
            if (EditedRequest == null)
            {
                return NotFound();
            }
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                    //This need to be refactored
                    if (EditedRequest.Id == 0)
                    {
                        await _rp.SaveAsync(EditedRequest);
                    }
                    else
                    {
                        var er = await _rp.GetByIdAsync(EditedRequest.Id);
                        await _rp.Edit(er, EditedRequest.Title, EditedRequest.Description);
                    }
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
