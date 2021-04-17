using BugTracker.Persistance.Abstract;
using Domain.Entities;
using Infrastructure.Persistance;
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
        private readonly ApplicationDbContext _ctx;
        public RequestPersistance(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Delete(RequestItem req)
        {
            _ctx.Remove(req);
            _ctx.SaveChanges();
        }

        public async Task DeleteAsync(RequestItem req)
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

        public async Task Edit(RequestItem req, string title)
        {
            req.Title = title;
            await _ctx.SaveChangesAsync();
        }

        public List<RequestItem> GetAll()
        {
            return _ctx.Requests.ToList();
        }

        public Task<List<RequestItem>> GetAllAsync()
        {
            return _ctx.Requests.ToListAsync();
        }

        public List<RequestItem> GetAssignedToProject(int projectId)
        {
            return _ctx.Requests.Include(t => t.Project)
                .Where(t => t.ProjectId == projectId)
                .ToList();
        }

        public RequestItem GetById(int id)
        {
            return _ctx.Requests.Where(t => t.Id == id)
                .SingleOrDefault();
        }

        public async Task<RequestItem> GetByIdAsync(int? id)
        {
            return await _ctx.Requests.FindAsync(id);
        }

        public List<RequestItem> GetCreatedByAuthor(string author)
        {
            return _ctx.Requests    
                .Where(t => t.Author == author)
                .ToList();
        }

        public async Task<List<RequestItem>> GetCreatedByAuthorAsync(string author)
        {
            return await _ctx.Requests.Where(r => r.Author == author)
                .Include(r => r.Project)
                .ToListAsync();

        }

        //This is weird behavior of ef which doesnt save a record! Need to be verified
        public async Task SaveAsync(RequestItem req)
        {
            await _ctx.Requests.AddAsync(req);
            await _ctx.SaveChangesAsync();
        }

    }
}
