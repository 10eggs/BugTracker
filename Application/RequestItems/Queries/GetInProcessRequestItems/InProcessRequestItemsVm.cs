using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetInProcessRequestItems
{
    public class InProcessRequestItemsVm
    {
        public IList<InProcessRequestItemDto> InProcessRequests { get; set; }
    }
}
