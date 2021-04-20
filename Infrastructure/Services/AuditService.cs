using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuditService : IAuditService
    {
        public Task OnAfterSaveChanges(IApplicationDbContext context,List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach(var auditEntry in auditEntries)
            {
                foreach(var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                context.Audits.Add(auditEntry.ToAudit());
            }

            return ((DbContext)context).SaveChangesAsync();
        }

        public List<AuditEntry> OnBeforeSaveChanges(IApplicationDbContext context)
        {
            ((DbContext)context).ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            
            foreach(var entry in ((DbContext)context).ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                //Do I need this line?
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.GetTableName();
                auditEntries.Add(auditEntry);

                foreach(var property in entry.Properties)
                {
                    if(property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.State = "Added";
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.State = "Deleted";
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            auditEntry.State = "Modified";
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }

            }

            foreach(var auditEntry in auditEntries.Where(ae=>!ae.HasTemporaryProperties))
            {
                context.Audits.Add(auditEntry.ToAudit());
            }

            return auditEntries.Where(ae => ae.HasTemporaryProperties).ToList();
        }
    }



}
