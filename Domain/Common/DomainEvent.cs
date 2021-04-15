using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DataOccured = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DataOccured { get; protected set; } = DateTime.UtcNow;
    }
}
