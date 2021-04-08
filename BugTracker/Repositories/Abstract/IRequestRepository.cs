using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Repositories.Abstract
{
    interface IRequestRepository:IRepository<Request>
    {
        public Task AddAsync(Request req);
        Task<Request> GetAsync(int? i);
        public Task<IEnumerable<Request>> GetAllAsync();
        public IEnumerable<Request> GetCreatedByAuthor(string author);
        public Task<IEnumerable<Request>> GetCreatedByAuthorAsync(string author);
        public IEnumerable<Request> GetAssignedToProject(int projectId);
        public Task Edit(Request req);
        public bool DeleteById(int id);
        public Task<bool> DeleteByIdAsync(int id);
        public bool Delete(Request t);
        public Task<bool> DeleteAsync(Request t);

    }
}
