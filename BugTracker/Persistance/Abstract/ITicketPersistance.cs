using Domain.Entities;
using Domain.Entities.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Persistance
{
    public interface ITicketPersistance
    {
        //Remove 'ticket' word
        public void SaveTicket(Ticket t);
        public void SaveAssigned(int ticketId, QA qa);
        public void SaveAssigned(int ticketId, Ticket ticket);
        Task Save(Ticket t);
        Task<Ticket> GetByIdAsync(int? i);
        Ticket GetById(int i);
        public List<Ticket> GetAll();
        public List<Ticket> GetCreatedByAuthor(string author);
        public Task<List<Ticket>> GetCreatedByAuthorAsync(string author);
        public Task<List<Ticket>> GetByRequestAuthor(string author);
        public List<Ticket> GetAssignedToProject(int projectId);
        public Task Edit(Ticket t, string title);
        public Task DeleteById(int id);
        public Task Delete(Ticket t);

        public Task<Ticket> FindById(int id);
        public Task UpdateTicket();

    }
}

