using BugTracker.DB;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerWeb.Controllers
{
    //Previous
    [Route("api/ProjectOwnerView/{projectId:int}")]
    [ApiController]

    public class POViewController : Controller
    {
        private AppDbContext _ctx;
        private IProjectPersistance _pp;
        private IRequestPersistance _rp;
        public POViewController(AppDbContext ctx,IProjectPersistance pp, IRequestPersistance rp)
        {
            _ctx = ctx;
            _pp = pp;
            _rp = rp;
        }

        //Need to be configured
        //[Route("api/notexist/{projectId:int}")]

        //[HttpGet]
        //public async Task<JsonResult> GetAllTicketsForProject(int projectId)
        //{
        //    using (_ctx)
        //    {
        //        //Mock
        //        //Get all tickets
        //        //var data = await _pp.GetRelatedTicketsAsync(projectId);

        //        //Get assigned tickets
        //        var data = await _pp.GetRelatedUnassignedTicketsAsync(projectId);

        //        //Get unassigned tickets
        //        //var data = await _pp.GetRelatedUnassignedTicketsAsync(projectId);
        //        return Json(new { data });
        //    }

        //}
        [HttpGet]
        public async Task<JsonResult> GetAllRequestsForProject(int projectId)
        {
            var data = await _pp.GetRelatedRequestsAsync(projectId);
            return Json(new { data });

        }

    }
}
