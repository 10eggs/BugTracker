using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.RequestItems.Queries.GetPendingRequestItems;
using Application.RequestItems.Queries.GetRequestItemDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebUI.Pages.ProjectOwnerView
{
    public class PendingRequestsModel : PageModel
    {
        private readonly IMediator _mediator;
        public PendingRequestsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public GetPendingRequestItemsQuery GetPendingRequestItemsQuery { get; set; }

        [BindProperty]
        public PendingRequestItemsListVm PendingRequestItemsListVm { get; set; }

        public async Task OnGet()
        {
            PendingRequestItemsListVm = await _mediator.Send(new GetPendingRequestItemsQuery());
        }

        public async Task<PartialViewResult> OnGetRequestItemDetails(int id)
        {
            var requestList = await _mediator.Send(new GetRequestItemDetailsQuery { RequestId = id });
            var singleRequest = requestList.Requests.FirstOrDefault();

            return new PartialViewResult
            {
                ViewName = "UserView/_RequestItemDetails",
                ViewData = new ViewDataDictionary<PendingRequestItemDto>(ViewData, singleRequest)
            };

        }
    }
}
