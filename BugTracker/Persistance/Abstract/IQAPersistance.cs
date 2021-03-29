using BugTracker.Models;
using System.Collections.Generic;

namespace BugTracker.Persistance
{
    public interface IQAPersistance
    {
        public QA Get(int id);

        public QA GetByName(string name);

        public QA GetByUserId(string userId);
        public void Save(QA qa);
        public ICollection<QA> GetAll();


    }
}
