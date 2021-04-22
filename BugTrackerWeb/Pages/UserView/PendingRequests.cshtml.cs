using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.RequestItems.Commands.DeleteRequestItem;
using Application.RequestItems.Queries.GetPendingRequestItems;
using BugTracker.Persistance.Abstract;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BugTrackerWeb.Pages.UserView
{
    public class PendingRequestModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public PendingRequestModel(IMediator mediator,IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public GetPendingRequestItemsQuery GetPendingRequestItemsQuery { get; set; }

        public PendingRequestItemsListVm PendingRequestItemsListVm { get; set; }


        public async Task OnGet()
        {
            PendingRequestItemsListVm = await _mediator.Send(new GetPendingRequestItemsQuery());
            _cache.Set("RequestItems", JsonConvert.SerializeObject(PendingRequestItemsListVm.Requests));

        }


        public async Task<PartialViewResult> OnGetRequestItemDetails(int id)
        {
            IList<PendingRequestItemDto> requests = JsonConvert.DeserializeObject<List<PendingRequestItemDto>>(_cache.Get("RequestItems").ToString());

            PendingRequestItemDto request = requests.Where(r => r.Id == id)
                .FirstOrDefault();

            return new PartialViewResult
            {
                ViewName = "_RequestItemDetails",
                ViewData = new ViewDataDictionary<PendingRequestItemDto>(ViewData, request)
            };

        }

        public async Task<JsonResult> OnPostDelete(int id)
        {
            return await _mediator.Send(new DeleteRequestItemCommand { Id = id });

        }




        //public async Task<JsonResult> OnGetPopulateTable()
        //{
        //    var requests = await _rp.GetCreatedByAuthorAsync(User.Identity.Name);
        //    var data = RequestModelView.CreateModelView(requests);

        //    return new JsonResult(new { data });
        //}

    }

    //public class RequestModelView
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public string Description { get; set; }
    //    public string Project { get; set; }
    //    private static IEnumerable<RequestModelView> Transform(List<RequestItem> requests)
    //    {
    //        foreach (var r in requests)
    //        {
    //            yield return new RequestModelView { Id = r.Id, Title = r.Title, Project = r.Project.Name };
    //        }
    //    }

    //    public static List<RequestModelView> CreateModelView(List<RequestItem> requests)
    //    {
    //        return Transform(requests).ToList();
    //    }
    //}
}
