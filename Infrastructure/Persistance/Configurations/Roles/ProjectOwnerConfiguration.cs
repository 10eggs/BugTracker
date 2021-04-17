using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configurations.Roles
{
    public class ProjectOwnerConfiguration : IEntityTypeConfiguration<ProjectOwner>
    {
        public void Configure(EntityTypeBuilder<ProjectOwner> builder)
        {
            builder.HasMany(b => b.Projects);

        }
    }
}
