using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.QAView
{
    public class IndexModel : PageModel
    {
        private readonly IQAPersistance _qap;
        private UserManager<IdentityUser> _um;

        public IndexModel(IQAPersistance qap,UserManager<IdentityUser> um)
        {
            _qap = qap;
            _um = um;
        }
        [BindProperty]
        public QA QA { get; set; }
        public void OnGet()
        {
            QA = _qap.GetByUserId(_um.GetUserId(HttpContext.User));

        }
    }
}
