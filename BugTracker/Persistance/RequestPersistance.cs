using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Persistance
{
    public class RequestPersistance : IRequestPersistance
    {
        private readonly AppDbContext _ctx;
        public RequestPersistance(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Delete(Request req)
        {
            _ctx.Remove(req);
            _ctx.SaveChanges();
        }

        public async Task DeleteAsync(Request req)
        {
            _ctx.Remove(req);
            await _ctx.SaveChangesAsync();
        }

        public void DeleteById(int id)
        {
            var request =  GetById(id) ?? throw new NullReferenceException();
            _ctx.Requests.Remove(request);
            _ctx.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var request = GetById(id) ?? throw new NullReferenceException();
            _ctx.Requests.Remove(request);
            await _ctx.SaveChangesAsync();
        }

        public async Task Edit(Request req, string title, string description)
        {
            req.Title = title;
            req.Description = description;
            await _ctx.SaveChangesAsync();
        }

        public List<Request> GetAll()
        {
            return _ctx.Requests.ToList();
        }

        public Task<List<Request>> GetAllAsync()
        {
            return _ctx.Requests.ToListAsync();
        }

        public List<Request> GetAssignedToProject(int projectId)
        {
            return _ctx.Requests.Include(t => t.Project)
                .Where(t => t.ProjectId == projectId)
                .ToList();
        }

        public Request GetById(int id)
        {
            return _ctx.Requests.Where(t => t.Id == id)
                .SingleOrDefault();
        }

        public async Task<Request> GetByIdAsync(int? id)
        {
            return await _ctx.Requests.FindAsync(id);
        }

        public List<Request> GetCreatedByAuthor(string author)
        {
            return _ctx.Requests    
                .Where(t => t.Author == author)
                .ToList();
        }

        public async Task<List<Request>> GetCreatedByAuthorAsync(string author)
        {
            return await _ctx.Requests.Where(r => r.Author == author)
                .Include(r => r.Project)
                .ToListAsync();

        }

        //This is weird behavior of ef which doesnt save a record! Need to be verified
        public async Task SaveAsync(Request req)
        {
            await _ctx.Requests.AddAsync(req);
            await _ctx.SaveChangesAsync();
        }

    }
}
