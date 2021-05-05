using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Persistance;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.Dashboard
{
    public class EditTicketModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ITicketPersistance tp;
        public EditTicketModel(ApplicationDbContext context)
        {
            _ctx = context;
           

        }

        [BindProperty]
        public Ticket EditedTicket { get; set; }
        public async Task OnGet(int id)
        {
            using(_ctx)
            {
                var tp =new TicketPersistance(_ctx);
                EditedTicket = await tp.GetByIdAsync(id);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            //hidden input for id
            //Read about tags helpers
            if (ModelState.IsValid)
            {
                using (_ctx)
                {
                    var tp = new TicketPersistance(_ctx);
                    var et = await tp.GetByIdAsync(EditedTicket.Id);

                    //await tp.Edit(et, EditedTicket.Title);
                    await _ctx.SaveChangesAsync();
                }
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
