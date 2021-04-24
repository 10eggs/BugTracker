using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.PageManagers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Domain.Entities.Roles;
using MediatR;
using Application.Projects.Queries.GetAllProjects;
using Application.Projects.Queries.GetTeamMembersForProject;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Application.Qa.Queries.GetAvailableQas;

namespace BugTrackerWeb.Pages.ProjectOwnerView
{
    public class ManageUsersModel : PageModel
    {
        IQAManager _qam;
        private readonly IMediator _mediator;

        public ManageUsersModel(IMediator mediator, IQAManager qam)
        {
            _qam = qam;
            _mediator = mediator;
        }
        public ICollection<QA> AllQAs { get; set; }
        public ICollection<QA> ProjectQAs { get; set; }

        public int ProjectId { get; set; }
        //public void OnGet(int id)
        //{
        //    Debug.WriteLine("Project from query string is " + id);
        //    ProjectId = id;
        //    AllQAs = _qam.GetAllQAs();
        //    ProjectQAs = _qam.GetQAsForProject(id);
        //}
        
        [BindProperty]
        public AllProjectsVm AllProjectsVm { get; set; }
        public async Task OnGet()
        {
            AllProjectsVm = await _mediator.Send(new GetAllProjectsQuery());
        }

        public async Task<PartialViewResult> OnGetTeamMembers(int id)
        {
            var memberList = await _mediator.Send(new GetTeamMembersForProjectRequest { ProjectId = id });

            return new PartialViewResult
            {
                ViewName = "Shared/ProjectOwner/_ManageTeamPartial",
                ViewData = new ViewDataDictionary<TeamMembersVm>(ViewData, memberList)
            };
        }

        public async Task<PartialViewResult> OnGetAddMembers()
        {
            var availableQasList = await _mediator.Send(new GetAvailableQasRequest());
            return new PartialViewResult
            {
                ViewName = "Shared/ProjectOwner/_AddTeamMemberPartial",
                ViewData = new ViewDataDictionary<TeamMembersVm>(ViewData, null)
            };

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
