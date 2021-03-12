using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BugTrackerTests
{
    public class ProjectHandlerTests
    {
        [Fact]
        public void GetQAsAssignedToTheProject()
        {
            using(var db = DbContextFactory.Create(nameof(GetQAsAssignedToTheProject)))
            {
                IProjectHandler iph = new ProjectHandler(db);
            }
            
            //arrange
            //act
            //assert
        }
    }

    internal class ProjectHandler : IProjectHandler
    {
        private IProjectPersistance _ipp;
        private IQAPersistance _qap;
        public ProjectHandler(AppDbContext ctx)
        {
            _ipp = new ProjectPersistance(ctx);
            _qap = new QAPersistance(ctx);
        }

        public void AssignQaToTheProject(QA qa, int projectId)
        {

        }

        public ICollection<QA> GetAllQAs()
        {
            return _qap.GetAll();
        }

        public ICollection<QA> GetQAsForProject(int projId)
        {
            return _ipp.GetAssignedQAs(projId);
        }
    }

    internal interface IProjectHandler
    {
        ICollection<QA> GetQAsForProject(int projId);
        ICollection<QA> GetAllQAs();

        void AssignQaToTheProject(QA qa, int projectId);
    }
}
