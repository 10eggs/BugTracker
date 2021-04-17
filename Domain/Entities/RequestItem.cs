using Domain.Common;
using Domain.Events.RequesItemEvents;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class RequestItem: AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StepsToReproduce { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string Author { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        private bool _assigned;
        public bool Assigned
        { 
            get=>_assigned;
            set
            {
                if(value == true && _assigned == false)
                {
                    DomainEvents.Add(new RequestItemAssignedEvent(this));
                }

                _assigned = value;
            }

        }
        public byte[] Attachements { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}

