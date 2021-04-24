using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetTeamMembersForProject
{
    public class TeamMembersVm
    {
        public IEnumerable<QaDto> Qas { get; set; }
    }
}
