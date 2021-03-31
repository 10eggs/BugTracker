﻿using BugTracker.DB;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Persistance
{
    public class QAPersistance : IQAPersistance
    {
        private AppDbContext _ctx;
        public QAPersistance(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public QA Get(int id)
        {
            return _ctx.QA.Where(q => q.Id == id)
                .SingleOrDefault();
        }
        public QA GetByUserId(string userId)
        {
            return _ctx.QA
                .Where(q => q.UserId == userId)
                .Include(q=>q.Projects)
                .Include(q => q.Tickets)
                    .SingleOrDefault();
        }

        public QA GetByName(string name)
        {
            return _ctx.QA.Where(q => q.Name == name)
                .SingleOrDefault();
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
