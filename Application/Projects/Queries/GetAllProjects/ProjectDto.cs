using Application.Common.Mappings;
using Domain.Entities;
using Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetAllProjects
{
    public class ProjectDto:IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
