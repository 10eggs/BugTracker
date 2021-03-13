using BugTracker.Models;
using System.Collections.Generic;

namespace BugTrackerTests
{
    public interface IQAPersistance
    {
        void Save(QA qa);
        public QA Get(int id);

        ICollection<QA> GetAll();

    }
}
