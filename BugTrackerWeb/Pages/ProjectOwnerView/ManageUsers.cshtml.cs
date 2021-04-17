using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.PageManagers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Domain.Entities.Roles;

namespace BugTrackerWeb.Pages.ProjectOwnerView
{
    public class ManageUsersModel : PageModel
    {
        IQAManager _qam;
        public ManageUsersModel(IQAManager qam)
        {
            _qam = qam;
        }
        public ICollection<QA> AllQAs { get; set; }
        public ICollection<QA> ProjectQAs { get; set; }

        public int ProjectId { get; set; }
        public void OnGet(int id)
        {
            Debug.WriteLine("Project from query string is " + id);
            ProjectId = id;
            AllQAs = _qam.GetAllQAs();
            ProjectQAs = _qam.GetQAsForProject(id);
        }

        public IActionResult OnPost(int qaId, int projectId)
        {
            _qam.AssignQaToTheProject(qaId, projectId);
            return RedirectToPage(new { id = projectId });
        }

        public IActionResult OnPostDischargeQa(int qaId, int projectId)
        {
            _qam.DeleteQaFromTheProject(qaId, projectId);
            return RedirectToPage(new { id = projectId });
        }
    }
}
