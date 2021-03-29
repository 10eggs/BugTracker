using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.QAView
{
    public class ProjectDetailsModel : PageModel
    {
        public ProjectDetailsModel()
        {

        }
        [BindProperty]
        public Project Project { get; set; }
        public void OnGet(Project project)
        {
            Project = project;
        }
    }
}
