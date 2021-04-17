using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Persistance.Abstract
{
    public interface IRequestPersistance
    {
        public Task SaveAsync(RequestItem req);
        //Task Save(Request req);
        Task<RequestItem> GetByIdAsync(int? i);
        RequestItem GetById(int i);
        public List<RequestItem> GetAll();
        public Task<List<RequestItem>> GetAllAsync();
        public List<RequestItem> GetCreatedByAuthor(string author);
        public Task<List<RequestItem>> GetCreatedByAuthorAsync(string author);
        public List<RequestItem> GetAssignedToProject(int projectId);
        public Task Edit(RequestItem t, string title);
        public void DeleteById(int id);
        public Task DeleteByIdAsync(int id);
        public void Delete(RequestItem t);
        public Task DeleteAsync(RequestItem t);

    }
}
