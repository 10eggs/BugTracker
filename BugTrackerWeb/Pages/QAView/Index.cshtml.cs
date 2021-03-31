using System.Collections.Generic;
using System.Linq;
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
        private readonly ITicketPersistance _tp;
        private UserManager<IdentityUser> _um;

        public IndexModel(IQAPersistance qap,ITicketPersistance tp,UserManager<IdentityUser> um)
        {
            _qap = qap;
            _um = um;
            _tp = tp;

        }
        [BindProperty]
        public QA QA { get; set; }

        [BindProperty]
        public List<Project> Projects { get; set; }


        public void OnGet()
        {
            QA = _qap.GetByUserId(_um.GetUserId(HttpContext.User));
            Projects = QA.Projects;

        }

        public IActionResult OnGetProjectDetails(int projectId, int qaId)
        {
            return RedirectToPage("ProjectDetails", new { projectid = projectId, qaid = qaId });

        }
    }

}
