using BugTracker.DB;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Persistance
{
    public class TicketPersistance : ITicketPersistance
    {
        private readonly AppDbContext _ctx;
        public TicketPersistance(AppDbContext context)
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

        public List<Ticket> GetAssignedToProject(string projectName)
        {
            return _ctx.Tickets.Include(t => t.Project)
                .Where(t => t.Project.Name == projectName)
                .ToList();
        }

        public List<Ticket> GetCreatedByAuthor(string author)
        {
            return _ctx.Tickets
                .Where(t => t.Author == author)
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

        public async Task<Ticket> GetById(int? i)
        {
            return await _ctx.Tickets.FindAsync(i);
        }

        public async Task Edit(Ticket t, string title, string description)
        {
            t.Title = title;
            t.Description = description;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var t = await GetById(id) ?? throw new NullReferenceException();
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
            return await _ctx.Tickets.Where(t => t.Author == author).ToListAsync();
        }
    }
}

