using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configurations
{
    public class RequestItemConfiguration : IEntityTypeConfiguration<RequestItem>
    {
        public void Configure(EntityTypeBuilder<RequestItem> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(r => r.Title)
                .IsRequired();

            builder.Property(r => r.StepsToReproduce)
                .IsRequired();

            builder.Property(r => r.ExpectedResult)
                .IsRequired();

            builder.Property(r => r.ActualResult)
                .IsRequired();

            builder.Property(r => r.Author)
                .IsRequired();

            builder.HasOne(r => r.Project)
                .WithMany(p => p.Requests);
        }
    }
}
