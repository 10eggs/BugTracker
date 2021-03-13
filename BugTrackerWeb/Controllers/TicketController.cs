using BugTracker.DB;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerWeb.Controllers
{
    [Route("api/Ticket")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly AppDbContext _ctx;
        public TicketController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            //return Json(new { data = _ctx.Tickets.ToList() });
            using (_ctx)
            {
                var tp = new TicketPersistance(_ctx);
                var data = await tp.GetAllAsync();
                return Json(new { data });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (_ctx)
            {
                var tp = new TicketPersistance(_ctx);
                var bookFromDb = await tp.GetByIdAsync(id);
                if(bookFromDb == null)
                {
                    return Json(new { success = false, message = "Error while Deleting" });

                }
                await tp.Delete(bookFromDb);
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
