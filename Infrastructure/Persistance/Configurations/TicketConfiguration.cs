using Domain.Entities;
using Domain.Enums.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.Configurations
{
    public class TicketConfiguration: IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {

            builder.Ignore(e => e.DomainEvents);


            //builder.Property(t => t.Title)
            //    .IsRequired();

            //builder.Property(t => t.StepsToReproduce)
            //.IsRequired();

            //builder.Property(t => t.ExpectedResult)
            //    .IsRequired();

            //builder.Property(t => t.Author)
            //.IsRequired();

            builder.HasOne(e => e.Project)
                .WithMany(e => e.Tickets);

            builder.Navigation(b => b.Project);

            builder.HasOne(e => e.Qa);

            builder.HasOne(e => e.RequestItem);

            builder.Property(at => at.TicketStatus)
            .HasConversion(at => at.ToString(),
            at => (TicketStatus)Enum.Parse(typeof(TicketStatus), at));

            builder.Property(at => at.TicketPriority)
            .HasConversion(at => at.ToString(),
            at => (TicketPriority)Enum.Parse(typeof(TicketPriority), at));

            builder.Property(at => at.TicketCategory)
            .HasConversion(at => at.ToString(),
            at => (TicketCategory)Enum.Parse(typeof(TicketCategory), at));

        }
    }
}
