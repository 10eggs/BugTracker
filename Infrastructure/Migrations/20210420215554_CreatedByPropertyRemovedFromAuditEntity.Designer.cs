﻿// <auto-generated />
using System;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210420215554_CreatedByPropertyRemovedFromAuditEntity")]
    partial class CreatedByPropertyRemovedFromAuditEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Common.DomainEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DataOccured")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<int?>("RequestItemId")
                        .HasColumnType("int");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestItemId");

                    b.HasIndex("TicketId");

                    b.ToTable("DomainEvent");
                });

            modelBuilder.Entity("Domain.Entities.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("KeyValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectOwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectOwnerId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Domain.Entities.RequestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Assigned")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Attachements")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpectedResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("StepsToReproduce")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Domain.Entities.Roles.ProjectOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectOwner");
                });

            modelBuilder.Entity("Domain.Entities.Roles.QA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("QA");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpectedResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("QaID")
                        .HasColumnType("int");

                    b.Property<string>("RequestAuthor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StepsToReproduce")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketCategory")
                        .HasColumnType("int");

                    b.Property<int>("TicketPriority")
                        .HasColumnType("int");

                    b.Property<int>("TicketSeverity")
                        .HasColumnType("int");

                    b.Property<int>("TicketStatus")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("QaID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ProjectQA", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("QAsId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsId", "QAsId");

                    b.HasIndex("QAsId");

                    b.ToTable("ProjectQA");
                });

            modelBuilder.Entity("Domain.Common.DomainEvent", b =>
                {
                    b.HasOne("Domain.Entities.RequestItem", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("RequestItemId");

                    b.HasOne("Domain.Entities.Ticket", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("TicketId");
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.Ticket", "Ticket")
                        .WithMany("Comments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.HasOne("Domain.Entities.Roles.ProjectOwner", "ProjectOwner")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectOwner");
                });

            modelBuilder.Entity("Domain.Entities.RequestItem", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("Requests")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("Tickets")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Roles.QA", "Qa")
                        .WithMany("Tickets")
                        .HasForeignKey("QaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Qa");
                });

            modelBuilder.Entity("ProjectQA", b =>
                {
                    b.HasOne("Domain.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Roles.QA", null)
                        .WithMany()
                        .HasForeignKey("QAsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Domain.Entities.RequestItem", b =>
                {
                    b.Navigation("DomainEvents");
                });

            modelBuilder.Entity("Domain.Entities.Roles.ProjectOwner", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Domain.Entities.Roles.QA", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DomainEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
