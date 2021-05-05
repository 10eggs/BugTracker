using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tickets.Queries.GetTickets
{
    public class TicketDto:IMapFrom<Ticket>
    {
        public int Id { get; set; }
        public string RequestTitle { get; set; }
        public string TicketAuthor { get; set; }
        public string QaName { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public TicketSeverity TicketSeverity { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ticket, TicketDto>()
                .ForMember(d => d.RequestTitle, opt => opt.MapFrom(s => s.RequestItem.Title));

            profile.CreateMap<Ticket, TicketDto>()
                .ForMember(d => d.QaName, opt => opt.MapFrom(s => s.Qa.Name));
        }
    }
}
