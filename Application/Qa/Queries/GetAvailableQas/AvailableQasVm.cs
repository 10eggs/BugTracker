using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Qa.Queries.GetAvailableQas
{
    public class AvailableQasVm
    {
        public int ProjectId { get; set; }
        public IEnumerable<QaDto> AvailableQas { get; set; }
    }
}
