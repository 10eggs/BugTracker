using BugTracker.DB;
using BugTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackerTests
{
    public class QAPersistance : IQAPersistance
    {
        private AppDbContext _ctx;
        public QAPersistance(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<QA> GetAll()
        {
            return _ctx.QA.ToList();
        }

        public void Save(QA qa)
        {
            _ctx.QA.Add(qa);
            _ctx.SaveChanges();
        }
    }
}
