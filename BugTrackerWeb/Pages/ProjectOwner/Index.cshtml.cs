using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.ProjectOwner
{
    public class IndexModel : PageModel
    {
        private AppDbContext _ctx;
        private IProjectOwnerPersistance _pop;

        public IndexModel(AppDbContext ctx, IProjectOwnerPersistance pop)
        {
            _ctx = ctx;
            _pop = pop;
        }
        public BugTracker.Models.ProjectOwner ProjectOwner { get; set; }
        List<Project> Projects { get; set; }
        List<IdentityUser> Personel { get; set; }
        public void OnGet()
        {
            //ProjectOwner = _pop.GetProjectOwnerWithRelatedProjects(User.Identity.Name);
          
        }
    }
}
