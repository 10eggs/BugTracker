using System.Collections.Generic;
using System.Threading.Tasks;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWeb.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ITicketPersistance _tp;
        private readonly IRequestPersistance _rp;
        public IndexModel(ApplicationDbContext ctx, ITicketPersistance tp, IRequestPersistance rp)
        {
            _ctx = ctx;
            _tp = tp;
            _rp = rp;
        }

        public List<RequestItem> Requests { get; set; }


        public async Task OnGet()
        {
            //UserRequests = await _rp.GetCreatedByAuthorAsync(User.Identity.Name);
            Requests = await _rp.GetAllAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _rp.DeleteByIdAsync(id);
            return RedirectToPage("Index");
        }
    }
}
