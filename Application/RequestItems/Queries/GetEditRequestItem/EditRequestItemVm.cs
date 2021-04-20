using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetEditRequestItem
{
    public class EditRequestItemVm
    {
        public int Id { get; set; }
        public IDictionary<int, string> AvailableProjects { get; set; }

    }
}
