using BugTracker.DB;
using BugTracker.Models;
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
                var qasList = iph.GetQAs(1);
                Assert.NotNull(qasList);
            }
            
            //arrange
            //act
            //assert
        }
    }

    internal class ProjectHandler : IProjectHandler
    {
        private IProjectPersistance _ipp;
        public ProjectHandler(AppDbContext ctx)
        {
            _ipp = new ProjectPersistance(ctx);
        }
        public ICollection<QA> GetQAs(int projId)
        {
            return _ipp.GetAssignedQAs(projId);
        }
    }

    internal interface IProjectHandler
    {
        ICollection<QA> GetQAs(int projId);
    }
}
