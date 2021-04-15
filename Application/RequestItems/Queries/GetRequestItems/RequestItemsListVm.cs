using System;
using System.Collections.Generic;
using System.Text;

namespace Application.RequestItems.Queries.GetRequestItems
{
    public class RequestItemsListVm
    {
        public IList<RequestItemsListDto> Requests { get; set; }
    }
}
