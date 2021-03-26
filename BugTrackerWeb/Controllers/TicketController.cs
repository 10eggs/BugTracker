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
    [Route("api/Requests")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly AppDbContext _ctx;
        private readonly IRequestPersistance _rp;
        public TicketController(AppDbContext ctx, IRequestPersistance rp)
        {
            _ctx = ctx;
            _rp = rp;
            
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            using (_ctx)
            {
                var rp = new RequestPersistance(_ctx);
                var data = await rp.GetAllAsync();
                return Json(new { data });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (_ctx)
            {
                var rp = new RequestPersistance(_ctx);
                var reqToDelete = await rp.GetByIdAsync(id);
                if(reqToDelete == null)
                {
                    return Json(new { success = false, message = "Error while Deleting" });

                }
                await rp.Delete(reqToDelete);
                return Json(new { success = true, message = "Delete successful" });

            }
        }

        //[HttpGet]
        //public  IActionResult GetAll()
        //{
        //    //return Json(new { data = _ctx.Tickets.ToList() });
        //    using (_ctx)
        //    {
        //        var tp = new TicketPersistance(_ctx);
        //        var data = tp.GetAll();
        //        return Json(new { data });
        //    }
        //}

    }
}
