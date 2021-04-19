using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.RequestItems.Queries.GetPendingRequestItems
{
    public class PendingRequestItemDto:IMapFrom<RequestItem>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StepsToReproduce { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string ProjectName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RequestItem, PendingRequestItemDto>()
                .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Project.Name));
        }
    }
}
