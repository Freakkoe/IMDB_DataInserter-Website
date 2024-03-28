﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using IMDB_Website.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace imdb_app.Models
{
    public partial class imdbContext : DbContext
    {
        public imdbContext()
        {
        }

        public imdbContext(DbContextOptions<imdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<KnownForTitle> KnownForTitles { get; set; }
        public virtual DbSet<Name> Names { get; set; }
        public virtual DbSet<NonAdult> NonAdults { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<Writer> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IMDBv1;Integrated Security=True;Connect Timeout=30;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToView("Actors");
            });

            modelBuilder.Entity<Director>(entity =>
            {
                entity.HasOne(d => d.NconstNavigation)
                    .WithMany(p => p.Directors)
                    .HasForeignKey(d => d.Nconst)
                    .HasConstraintName("FK_Directors_Names");

                entity.HasOne(d => d.TconstNavigation)
                    .WithMany(p => p.Directors)
                    .HasForeignKey(d => d.Tconst)
                    .HasConstraintName("FK_Directors_Titles");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasOne(d => d.TconstNavigation)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.Tconst)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Genres_Titles");
            });

            modelBuilder.Entity<KnownForTitle>(entity =>
            {
                entity.HasOne(d => d.NconstNavigation)
                    .WithMany(p => p.KnownForTitles)
                    .HasForeignKey(d => d.Nconst)
                    .HasConstraintName("FK_KnownForTitles_Names");

                entity.HasOne(d => d.TconstNavigation)
                    .WithMany(p => p.KnownForTitles)
                    .HasForeignKey(d => d.Tconst)
                    .HasConstraintName("FK_KnownForTitles_Titles");
            });

            modelBuilder.Entity<NonAdult>(entity =>
            {
                entity.ToView("Non Adult");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.HasOne(d => d.NconstNavigation)
                    .WithMany(p => p.Professions)
                    .HasForeignKey(d => d.Nconst)
                    .HasConstraintName("FK_Professions_Names");
            });

            modelBuilder.Entity<Writer>(entity =>
            {
                entity.HasOne(d => d.NconstNavigation)
                    .WithMany(p => p.Writers)
                    .HasForeignKey(d => d.Nconst)
                    .HasConstraintName("FK_Writers_Names");

                entity.HasOne(d => d.TconstNavigation)
                    .WithMany(p => p.Writers)
                    .HasForeignKey(d => d.Tconst)
                    .HasConstraintName("FK_Writers_Titles");
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}