using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuditService
    {
        public List<AuditEntry> OnBeforeSaveChanges(IApplicationDbContext context);
        public Task OnAfterSaveChanges(IApplicationDbContext context,List<AuditEntry> auditEntries);

    }
}
