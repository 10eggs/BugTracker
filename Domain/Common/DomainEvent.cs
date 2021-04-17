using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
    public class DomainEvent
    {
        public int Id { get; set; }
        protected DomainEvent()
        {
            DataOccured = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DataOccured { get; protected set; } = DateTime.UtcNow;
    }
}
