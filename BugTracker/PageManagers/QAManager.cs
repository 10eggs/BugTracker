using BugTracker.Persistance;
using Domain.Entities.Roles;
using Infrastructure.Persistance;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.PageManagers
{
    public class QAManager:IQAManager
    {
        ApplicationDbContext _ctx;

        private IProjectPersistance _ipp;
        private IQAPersistance _qap;
        public QAManager(ApplicationDbContext ctx)
        {
            _ipp = new ProjectPersistance(ctx);
            _qap = new QAPersistance(ctx);
            _ctx = ctx;
        }

        public void AssignQaToTheProject(int qaId, int projectId)
        {
            var qa = _qap.Get(qaId);
            _ipp.AssignQa(projectId,qa);
        }

        public void DeleteQaFromTheProject(int qaId, int projId)
        {
            //This will need to be refeactored later?
            var proj = _ipp.GetProject(projId);
            var qa = _qap.Get(qaId);
            proj.QAs.ToList().Remove(qa);
            _ctx.SaveChanges();

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
