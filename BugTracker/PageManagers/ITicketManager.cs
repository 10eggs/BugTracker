﻿using BugTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.PageManagers
{
    public interface ITicketManager
    {
        public void AssignToQa(int ticketId, int qaId);
        public void AssignToQa(int ticketId, string qaName);

        public void AssignToQa(int ticketId, AssignedTicket assignedTicket);
        public ICollection<AssignedTicket> GetAllAssignedForProject(int projectId);
        public ICollection<AssignedTicket> GetAllFromProjectAssignedToQa(int projectId, int qaId);



    }
}
