using Domain.Common;
using Domain.Entities;

namespace Domain.Events.RequesItemEvents
{
    public class RequestItemAssignedEvent: DomainEvent
    {
        public RequestItemAssignedEvent(RequestItem item)
        {
            Item = item;
        }
        public RequestItem Item { get; }
    }
}
