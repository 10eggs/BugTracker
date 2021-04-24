using Application.Common.Mappings;
using Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetTeamMembersForProject
{
    public class QaDto:IMapFrom<QA>
    {
        public string Name { get; set; }

    }
}
