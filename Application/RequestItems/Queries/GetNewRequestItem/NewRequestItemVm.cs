using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetNewRequestItem
{
    public class NewRequestItemVm
    {
        public IList<ProjectDto> AvailableProjects { get; set; }
    }
}
