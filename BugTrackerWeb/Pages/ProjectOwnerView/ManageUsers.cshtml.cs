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
using Application.Qa.Commands.AssignQa;
using Application.Qa.Commands.DischargeQa;

namespace BugTrackerWeb.Pages.ProjectOwnerView
{
    public class ManageUsersModel : PageModel
    {
        private readonly IMediator _mediator;

        public ManageUsersModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public AssignQaCommand AssignQaCommand { get; set; }

        [BindProperty]
        public AllProjectsVm AllProjectsVm { get; set; }

        [BindProperty]
        public TeamMembersVm TeamMembersVm { get; set; }

        [BindProperty]
        public AvailableQasVm AvailableQasVm { get; set; }

        [BindProperty]

        public DischargeQaCommand DischargeQaCommand { get; set; }


        public async Task OnGet()
        {
            AllProjectsVm = await _mediator.Send(new GetAllProjectsQuery());
        }

        public async Task<PartialViewResult> OnGetTeamMembers(int projectid)
        {
            TeamMembersVm = await _mediator.Send(new GetTeamMembersForProjectRequest { ProjectId = projectid });

            return new PartialViewResult
            {
                ViewName = "Shared/ProjectOwner/_ManageTeamPartial",
                ViewData = new ViewDataDictionary<ManageUsersModel>(ViewData, this)
            };

        }

        public async Task<PartialViewResult> OnGetAddMembers(int projectid)
        {
            ProjectId = projectid;
            AvailableQasVm = await _mediator.Send(new GetAvailableQasRequest { ProjectId = projectid });

            return new PartialViewResult
            {
                ViewName = "Shared/ProjectOwner/_AddTeamMemberPartial",
                ViewData = new ViewDataDictionary<ManageUsersModel>(ViewData, this)
            };

        }

        public async Task<IActionResult> OnPostAssignUser(AssignQaCommand assignQaCommand)
        {
            await _mediator.Send(assignQaCommand);
            return Redirect("ManageUsers");

        }

        public async Task<JsonResult> OnPostDischargeQa(int qaId, int projectId)
        {
            return await _mediator.Send(new DischargeQaCommand { ProjectId = projectId, QaId = qaId });

        }

    }
}
