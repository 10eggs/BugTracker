using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.UserView
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IRequestPersistance _rp;
        public UpsertModel(ApplicationDbContext context, IRequestPersistance rp)
        {
            _ctx = context;
            _rp = rp;
        }

        [BindProperty]
        public RequestItem EditedRequest { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            EditedRequest = new RequestItem();
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
                        await _rp.Edit(er, EditedRequest.Title);
                    }
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
