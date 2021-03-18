using BugTracker.DB;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerWeb.Controllers
{
    [Route("api/ProjectOwnerView/{projectId:int}")]
    [ApiController]

    public class POViewController : Controller
    {
        private AppDbContext _ctx;
        private IProjectPersistance _pp;
        public POViewController(AppDbContext ctx,IProjectPersistance pp)
        {
            _ctx = ctx;
            _pp = pp;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllTicketsForProject(int projectId)
        {
            using (_ctx)
            {
                var data = await _pp.GetRelatedTicketsAsync(projectId);
                return Json(new { data });
            }

        }

    }
}
