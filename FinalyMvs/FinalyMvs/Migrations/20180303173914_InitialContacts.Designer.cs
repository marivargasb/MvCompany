﻿// <auto-generated />
using FinalyMvs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FinalyMvs.Migrations
{
    [DbContext(typeof(FinalyMvsContext))]
    [Migration("20180303173914_InitialContacts")]
    partial class InitialContacts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalyMvs.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LastName");

                    b.Property<string>("Mail");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Position");

                    b.HasKey("ID");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("FinalyMvs.Models.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientsID");

                    b.Property<string>("LastName");

                    b.Property<string>("Mail");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Position");

                    b.HasKey("ID");

                    b.HasIndex("ClientsID");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("FinalyMvs.Models.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Card");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("PageWeb");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinalyMvs.Models.Contact", b =>
                {
                    b.HasOne("FinalyMvs.Models.Client", "Clients")
                        .WithMany()
                        .HasForeignKey("ClientsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
