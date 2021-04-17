using BugTracker.Models;
using BugTracker.Repositories.Abstract;
using Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Repositories
{
    class RequestRepository:Repository<Request>,IRequestRepository
    {
        public RequestRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public ApplicationDbContext Ctx
        {
            get { return Context as ApplicationDbContext; }

        }

        public Task AddAsync(Request req)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Request t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Request t)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(Request req)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Request>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetAssignedToProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetAsync(int? i)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetCreatedByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Request>> GetCreatedByAuthorAsync(string author)
        {
            throw new NotImplementedException();
        }
    }
}
