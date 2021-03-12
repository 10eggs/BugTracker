using BugTracker.Models;
using System.Collections.Generic;

namespace BugTrackerTests
{
    public interface IQAPersistance
    {
        void Save(QA qa);
        ICollection<QA> GetAll();

    }
}
