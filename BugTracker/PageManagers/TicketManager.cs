using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.PageManagers
{
    public class TicketManager : ITicketManager
    {
        private AppDbContext _ctx;
        private ITicketPersistance _tp;
        private IRequestPersistance _rp;
        private IQAPersistance _qap;
        public TicketManager(AppDbContext ctx, IRequestPersistance rp,ITicketPersistance tp, IQAPersistance qap)
        {
            _ctx = ctx;
            _tp = tp;
            _qap = qap;
            _rp = rp;

        }

        public async Task AssignTicket(int requestId,Ticket t)
        {
            _rp.DeleteById(requestId);
            _tp.SaveTicket(t);

        }

        public void AssignToQa(int ticketId, int qaId)
        {
            var qa = _qap.Get(qaId);
            _tp.SaveAssigned(ticketId,qa);
        }

        public void AssignToQa(int ticketId, Ticket assignedTicket)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Ticket> GetAllAssignedForProject(int projectId)
        {
            return _ctx.Tickets
                 .Include(t => t.Qa)
                 .Include(t => t.Project)
                .Where(t => t.ProjectId == projectId)
                .Select(t => t)
                .ToList();
        }

        public ICollection<Ticket> GetAllFromProjectAssignedToQa(int projectId, int qaId)
        {
            return _ctx.Tickets
                     .Include(t => t.Qa)
                     .Include(t => t.Project)
                    .Where(t => t.ProjectId == projectId && t.QaID==qaId)
                    .Select(t => t)
                    .ToList();
        }
    }
}
