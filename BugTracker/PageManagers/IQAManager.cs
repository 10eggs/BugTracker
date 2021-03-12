using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.PageManagers
{
    public interface IQAManager
    {
        public ICollection<QA> GetQAsForProject(int projId);
        public ICollection<QA> GetAllQAs();
        public void AssignQaToTheProject(int qaId, int projId);
        public void DeleteQaFromTheProject(int qaId, int projId);
    }
}
