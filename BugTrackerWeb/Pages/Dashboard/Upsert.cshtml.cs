using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.Dashboard
{
    public class UpsertModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private readonly ITicketPersistance tp;
        public UpsertModel(AppDbContext context)
        {
            _ctx = context;


        }

        [BindProperty]
        public Ticket EditedTicket { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            EditedTicket = new Ticket();
            if (id == null)
            {
                return Page();
            }
            using (_ctx)
            {
                var tp = new TicketPersistance(_ctx);
                EditedTicket = await tp.GetById(id);
                if (EditedTicket == null)
                {
                    return NotFound();
                }
            }
            return Page();
           
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
                    //This need to be refactored
                    if (EditedTicket.Id == 0)
                    {
                         tp.SaveTicket(EditedTicket);
                    }
                    else
                    {
                        //tp.Update would update every field in the book
                        var et = await tp.GetById(EditedTicket.Id);
                        await tp.Edit(et, EditedTicket.Title, EditedTicket.Description);
                    }

                }
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
