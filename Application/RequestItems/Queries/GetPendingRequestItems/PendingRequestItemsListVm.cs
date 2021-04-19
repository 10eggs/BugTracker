using System;
using System.Collections.Generic;
using System.Text;

namespace Application.RequestItems.Queries.GetPendingRequestItems
{
    public class PendingRequestItemsListVm
    {
        public IList<PendingRequestItemDto> Requests { get; set; }
    }
}
