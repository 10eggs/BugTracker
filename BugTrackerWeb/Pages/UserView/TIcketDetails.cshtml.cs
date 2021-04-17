using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Persistance;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.UserView
{
    public class TIcketDetailsModel : PageModel
    {
        private readonly ITicketPersistance _tp;
        public TIcketDetailsModel(ITicketPersistance tp)
        {
            _tp = tp;
        }
        [BindProperty]
        public Ticket Ticket { get; set; }
        public void OnGet(int id)
        {
            Ticket =_tp.GetById(id);
        }
    }
}
