using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerWeb.Pages.UserView
{
    public class NewRequestModel : PageModel
    {
        private readonly IRequestPersistance _rp;
        private readonly IProjectPersistance _pp;
        private readonly ICurrentUserService _currentUserService;
        public NewRequestModel(ApplicationDbContext context, IRequestPersistance rp, IProjectPersistance pp,ICurrentUserService userService)
        {
            _rp = rp;
            _pp = pp;
            _currentUserService = userService;
        }


        [BindProperty]
        public RequestItem NewRequest { get; set; }

        [BindProperty]
        public List<SelectListItem> ProjectListDDL { get; set; }

        [BindProperty]

        public List<TestDDLOption> TestDDL { get; set; }
        public async Task OnGet()
        {
            Debug.WriteLine("User id from the service: "+_currentUserService.UserEmail);
            ProjectListDDL = PopulateProjectList();
            TestDDL = new List<TestDDLOption>
            {
                new TestDDLOption{Id=1,Name="Damex"},
                new TestDDLOption{Id=2,Name="Damex2"}
            };

        }
        public async Task<IActionResult> OnPostSaveRequest()
        {
            if (ModelState.IsValid)
            {
                AssignUserToRequest();
                await _rp.SaveAsync(NewRequest);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
        public void AssignUserToRequest()
        {
            NewRequest.Author = User.Identity.Name;
        }

        public List<SelectListItem> PopulateProjectList()
        {

            return _pp.GetAll().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name

            }).ToList();
        }


    }

    public class TestDDLOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
