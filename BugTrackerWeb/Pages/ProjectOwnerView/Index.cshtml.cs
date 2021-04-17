using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Persistance;
using Domain.Entities;
using Domain.Entities.Roles;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.ProjectOwnerView
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _ctx;
        private IProjectOwnerPersistance _pop;
        private UserManager<IdentityUser> _um;

        public IndexModel(ApplicationDbContext ctx, IProjectOwnerPersistance pop, UserManager<IdentityUser> um)
        {
            _ctx = ctx;
            _pop = pop;
            _um = um;
        }
        public ProjectOwner ProjectOwner { get; set; }
        
        List<Project> Projects { get; set; }
        List<IdentityUser> Personel { get; set; }
        public void OnGet()
        {
            var id = _um.GetUserId(HttpContext.User);
            ProjectOwner = _pop.GetProjectOwnerWithRelatedProjects(id);

        }
    }
}
