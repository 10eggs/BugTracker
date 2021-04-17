using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace BugTrackerWeb.Controllers
{
    //Previous
    //[Route("api/ProjectOwnerView/{projectId:int}")]
    [ApiController]

    public class POViewController : Controller
    {
        private ApplicationDbContext _ctx;
        private readonly ITicketPersistance _tp;
        private IProjectPersistance _pp;
        private IRequestPersistance _rp;
        public POViewController(ApplicationDbContext ctx,IProjectPersistance pp, ITicketPersistance tp ,IRequestPersistance rp)
        {
            _ctx = ctx;
            _tp = tp;
            _pp = pp;
            _rp = rp;
        }

        [Route("api/ProjectOwnerView/Requests/{projectId:int}")]

        [HttpGet]
        public async Task<JsonResult> GetAllRequestsForProject(int projectId)
        {
            var data = await _pp.GetRelatedRequestsAsync(projectId);

            return Json(new { data });

        }

        [Route("api/ProjectOwnerView/Tickets/{projectId:int}")]
        [HttpGet]
        public async Task<JsonResult> GetAllTicketsForTheProject(int projectId)
        {
            var tickets = await _pp.GetRelatedTicketsAsync(projectId);
            var data = TicketView.CreateModelView(tickets);

            return Json(new { data });

        }

        public class TicketView
        {
            public string Id;
            public string Title { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
            public string QaName { get; set; }
            private static IEnumerable<TicketView> Transform(List<Ticket> tickets)
            {
                foreach(var t in tickets)
                {
                    yield return new TicketView { Id = t.Id.ToString(), Title = t.Title, Author = t.Author, QaName = t.Qa.Name };
                }
            }

            public static List<TicketView> CreateModelView(List<Ticket> tickets)
            {
                return Transform(tickets).ToList();
            }
        }


        ////FOR TEST PURPOSE
        //[Route("api/QaView/CheckMe/{rm?}")]
        //[HttpPost]
        //public async Task<JsonResult> OnPostUpdateTicket(ResponseModel rm)
        //{
        //    var d = rm;
        //    if (!ModelState.IsValid)
        //    {
        //        Debug.WriteLine("Model invalid");
        //    }
        //    var checkModel = Response;
        //    //var deserialize = JsonConvert.DeserializeObject<ResponseModel>(r);

        //    var checkIfModelChanged = Response;
        //    //ResponseModel response = JsonConvert.DeserializeObject<ResponseModel>(updatedTicketJson);
        //    /**
        //     * This metho should take json, deserialize it and return json when post positive
        //     */
        //    return new JsonResult(new { success = true, message = "Ticket has been assigned!", errormessage = "Something went wrong, try again later" });

        //}
    }
}
