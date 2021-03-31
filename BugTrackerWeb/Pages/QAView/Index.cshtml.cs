using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.QAView
{
    public class IndexModel : PageModel
    {
        private readonly IQAPersistance _qap;
        private readonly IModelDistributor _imd;
        private UserManager<IdentityUser> _um;

        public IndexModel(IQAPersistance qap,IModelDistributor imd,UserManager<IdentityUser> um)
        {
            _qap = qap;
            _um = um;
            _imd = imd;

        }
        [BindProperty]
        public QA QA { get; set; }

        [BindProperty]
        public List<Project> Projects { get; set; }

        public void OnGet()
        {
            QA = _qap.GetByUserId(_um.GetUserId(HttpContext.User));
            Projects= QA.Projects;
            _imd.SetData("projects", Projects);
            var something = "s";
            
        }

        //public IActionResult OnGetProjectDetails(int projectId,int qaId)
        //{
        //    var qA = QA;
        //    //.Select(p => p.Tickets
        //    //.Where(t => t.QaID == qaId)).ToList();
        //    return RedirectToPage("ProjectDetails", new { project = 1, projectid = projectId, qaid = qaId });

        //}
    }
}
