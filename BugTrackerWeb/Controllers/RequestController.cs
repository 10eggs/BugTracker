using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerWeb.Controllers
{
    //[Route("api/Requests")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly AppDbContext _ctx;
        private readonly IRequestPersistance _rp;
        public RequestController(AppDbContext ctx, IRequestPersistance rp)
        {
            _ctx = ctx;
            _rp = rp;
            
        }

        [Route("api/Requests")]

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            //Circulate reference
            var requests = await _rp.GetCreatedByAuthorAsync(User.Identity.Name);
            var data = RequestView.CreateModelView(requests);

            return Json(new { data });
        }

        [Route("api/Requests/Delete")]

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
                await rp.DeleteAsync(reqToDelete);
                return Json(new { success = true, message = "Delete successful" });

            }
        }

        public class RequestView
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Project { get; set; }
            //Delete later
            public string Author { get; set; }
            private static IEnumerable<RequestView> Transform(List<Request> requests)
            {
                foreach (var r in requests)
                {
                    yield return new RequestView { Id = r.Id, Title = r.Title, Description = r.Description, Project=r.Project.Name  };
                }
            }

            public static List<RequestView> CreateModelView(List<Request> requests)
            {
                return Transform(requests).ToList();
            }
        }

    }
}
