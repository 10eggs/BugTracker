﻿using BugTracker.Models;
using System.Collections.Generic;


namespace BugTracker.Persistance
{
    public interface IProjectPersistance
    {
        //How to access record from db - by passing reference or value
        public void Save(Project t);
        public Project Get(int id);
        public Project GetProject(int id);
        public List<Project> GetAll();
        public List<Project> GetOwnedBy(ProjectOwner author);
        public List<Ticket> GetRelatedTickets(Project project);
        public void AssignQa(int projectId, QA qa);
        public List<QA> GetAssignedQAs(int projId);
    }
}