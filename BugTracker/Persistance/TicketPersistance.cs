using Domain.Entities;
using Domain.Entities.Roles;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Persistance
{
    public class TicketPersistance : ITicketPersistance
    {
        private readonly ApplicationDbContext _ctx;
        public TicketPersistance(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public List<Ticket> GetAll()
        {
            
            return _ctx.Tickets.Select(t => t)
                .ToList();
        }
        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _ctx.Tickets.ToListAsync();
        }

        public List<Ticket> GetAssignedToProject(int projectId)
        {
            //Do I need reference to the Project itself ?
            return _ctx.Tickets.Include(t => t.Project)
                .Where(t => t.ProjectId == projectId)
                .ToList();
        }

        public List<Ticket> GetCreatedByAuthor(string author)
        {
            return _ctx.Tickets
                //.Where(t => t.Author == author)
                .ToList();
        }

        public void SaveTicket(Ticket t)
        {
            _ctx.Tickets.Add(t);
            _ctx.SaveChanges();
        }

        public async Task Save(Ticket t)
        {
            await _ctx.Tickets.AddAsync(t);
            await _ctx.SaveChangesAsync();

        }

        public Ticket GetById(int id)
        {
            return _ctx.Tickets.Where(t => t.Id == id)
                .SingleOrDefault();
        }
        
        //To verification


        public async Task<Ticket> GetByIdAsync(int? i)
        {
            return await _ctx.Tickets.FindAsync(i);
        }

        public async Task Edit(Ticket t, string title)
        {
            //t.Title = title;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var t = await GetByIdAsync(id) ?? throw new NullReferenceException();
            _ctx.Tickets.Remove(t);
            await _ctx.SaveChangesAsync();

        }

        public async Task Delete(Ticket t)
        {
            _ctx.Remove(t);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetCreatedByAuthorAsync(string author)
        {
            return await _ctx.Tickets.ToListAsync();
        }

        public async Task<List<Ticket>> GetByRequestAuthor(string author)
        {
            return await _ctx.Tickets.ToListAsync();
        }
        public void SaveAssigned(int ticketId, QA qa)
        {
            var ticket = _ctx.Tickets.Include(t=>t.Project)
                .Where(t=>t.Id==ticketId)
                .SingleOrDefault();

            //check this line
            var assignedTicket = new Ticket();

            _ctx.Tickets.Remove(ticket);
            _ctx.SaveChanges();

            _ctx.Tickets.Add(assignedTicket);
            _ctx.SaveChanges();
        }

        public void SaveAssigned(int ticketId, Ticket assignedTicket)
        {
            var ticToRemove = _ctx.Tickets
                .Where(t => t.Id == ticketId)
                .SingleOrDefault();

            _ctx.Tickets.Remove(ticToRemove);
            _ctx.SaveChanges();

            _ctx.Tickets.Add(assignedTicket);
            _ctx.SaveChanges();
        }

        public async Task<Ticket> FindById(int id)
        {
            return await _ctx.Tickets.FindAsync(id);
        }

        public async Task UpdateTicket()
        {
            await _ctx.SaveChangesAsync();
        }

    }
}

