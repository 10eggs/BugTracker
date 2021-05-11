using Application.Common.Interfaces;
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
        public string Title { get; set; }
        public string TicketAuthorEmail { get; set; }
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

            //Here I had a problem with mapping for 'title' property.
            //This problem has been solved by changing order of 'CreateMap' invocation,
            //It didn't work when title had been mapped before qaName, after swapping order
            //It's working as expected
            
            profile.CreateMap<Ticket, TicketDto>()
                .ForMember(d => d.QaName, opt => opt.MapFrom(s => s.Qa.Name));

            profile.CreateMap<Ticket, TicketDto>()
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.RequestItem.Title));


        }
    }
}
