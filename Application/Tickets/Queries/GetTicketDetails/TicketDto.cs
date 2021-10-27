using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tickets.Queries.GetTicketDetails
{
    public class TicketDto:IMapFrom<Ticket>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StepsToReproduce { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string Author { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public TicketSeverity TicketSeverity { get; set; }
        public string TicketAuthorEmail { get; set; }
        public string QaName { get; set; }
        public RequestItem RequestItem { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }

        //Add history

        public void Mapping(Profile profile)
        {
            //Can not map from the RequestItem, that's why we are using direct mapping of RequestItem
            profile.CreateMap<Ticket, TicketDto>()
                .ForMember(d => d.QaName, opt => opt.MapFrom(s => s.Qa.Name));

            
        }
    }
}
