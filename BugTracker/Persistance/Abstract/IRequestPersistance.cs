using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Persistance.Abstract
{
    public interface IRequestPersistance
    {
        public Task SaveAsync(Request req);
        //Task Save(Request req);
        Task<Request> GetByIdAsync(int? i);
        Request GetById(int i);
        public List<Request> GetAll();
        public Task<List<Request>> GetAllAsync();
        public List<Request> GetCreatedByAuthor(string author);
        public Task<List<Request>> GetCreatedByAuthorAsync(string author);
        public List<Request> GetAssignedToProject(int projectId);
        public Task Edit(Request t, string title, string description);
        public void DeleteById(int id);
        public Task DeleteByIdAsync(int id);
        public void Delete(Request t);
        public Task DeleteAsync(Request t);

    }
}
