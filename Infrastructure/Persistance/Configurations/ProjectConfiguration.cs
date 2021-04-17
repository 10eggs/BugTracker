using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            //builder.HasOne(e => e.ProjectOwner)
            //    .WithMany(e => e.Projects);

            //builder.Navigation(e => e.ProjectOwner);
        }
    }
}
