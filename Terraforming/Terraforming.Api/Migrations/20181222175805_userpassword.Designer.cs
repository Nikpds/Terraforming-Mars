﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Terraforming.Api.Database;

namespace Terraforming.Api.Migrations
{
    [DbContext(typeof(MsDataContext))]
    [Migration("20181222175805_userpassword")]
    partial class userpassword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Terraforming.Api.Models.Game", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Terraforming.Api.Models.GameScore", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AwardsPlaced");

                    b.Property<int>("AwardsWon");

                    b.Property<int>("Board");

                    b.Property<string>("GameId");

                    b.Property<int>("Milestones");

                    b.Property<int>("Place");

                    b.Property<int>("Points");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("GameScore");
                });

            modelBuilder.Entity("Terraforming.Api.Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("Title");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Terraforming.Api.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("ExternaLogin");

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Lastname");

                    b.Property<string>("PasswordHash");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("VerificationToken");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Terraforming.Api.Models.GameScore", b =>
                {
                    b.HasOne("Terraforming.Api.Models.Game", "Game")
                        .WithMany("GamePlayers")
                        .HasForeignKey("GameId");

                    b.HasOne("Terraforming.Api.Models.User", "User")
                        .WithMany("GameScores")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Terraforming.Api.Models.Team", b =>
                {
                    b.HasOne("Terraforming.Api.Models.User")
                        .WithMany("Teams")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}