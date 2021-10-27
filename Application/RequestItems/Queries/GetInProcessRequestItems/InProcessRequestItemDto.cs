using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetInProcessRequestItems
{
    public class InProcessRequestItemDto:IMapFrom<Ticket>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ticket, InProcessRequestItemDto>()
                .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Project.Name));

            profile.CreateMap<Ticket, InProcessRequestItemDto>()
                .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.ProjectId));

            profile.CreateMap<Ticket, InProcessRequestItemDto>()
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.RequestItem.Title));
        }
    }
}
