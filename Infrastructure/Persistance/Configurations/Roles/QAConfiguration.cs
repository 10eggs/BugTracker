using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configurations.Roles
{
    public class QAConfiguration : IEntityTypeConfiguration<QA>
    {
        public void Configure(EntityTypeBuilder<QA> builder)
        {
            builder.HasMany(b => b.Projects)
                .WithMany(p => p.QAs);
            builder.HasMany(b => b.Tickets)
              .WithOne(t => t.Qa);
        }
    }
}
