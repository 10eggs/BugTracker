using BugTracker.DB;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BugTrackerTests
{
    public class QAPersistanceTests
    {
        [Fact]
        public void GetAllTest()
        {
            var qa1 = new QA() { Id = 1, Name = "Jon" };
            var qa2 = new QA() { Id = 2, Name = "Jon" };
            using (var db = DbContextFactory.Create(nameof(GetAllTest)))
            {
                db.QA.AddRange(qa1, qa2);
                db.SaveChanges();
            }

            using (var db = DbContextFactory.Create(nameof(GetAllTest)))
            {
                IQAPersistance iqap = new QAPersistance(db);
                var qas=iqap.GetAll();
                Assert.Equal(2, qas.Count);
            }

        }

    }

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

    public interface IQAPersistance
    {
        void Save(QA qa);
        ICollection<QA> GetAll();

    }
}
