using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace BugTrackerWeb.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.IsInRole("ProjectOwner"))
            {
                return RedirectToPage("/ProjectOwnerView/Index");
            }
            else if (User.IsInRole("QA"))
            {
                return RedirectToPage("/QAView/Index");
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToPage("/UserView/Index");
            }
            return Page();
        }
    }
}
