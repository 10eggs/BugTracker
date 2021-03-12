using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.PageManagers
{
    public class QAManager:IQAManager
    {
        private IProjectPersistance _ipp;
        private IQAPersistance _qap;
        public QAManager(AppDbContext ctx)
        {
            _ipp = new ProjectPersistance(ctx);
            _qap = new QAPersistance(ctx);
        }

        public void AssignQaToTheProject(int qaId, int projectId)
        {
            var qa = _qap.Get(qaId);
            _ipp.AssignQa(projectId,qa);
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
}
