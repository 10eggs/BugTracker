using BugTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.PageManagers
{
    public interface ITicketManager
    {
        public void AssignToQa(int ticketId, int qaId);

        public void AssignToQa(int ticketId, Ticket assignedTicket);
        public ICollection<Ticket> GetAllAssignedForProject(int projectId);
        public ICollection<Ticket> GetAllFromProjectAssignedToQa(int projectId, int qaId);



    }
}
