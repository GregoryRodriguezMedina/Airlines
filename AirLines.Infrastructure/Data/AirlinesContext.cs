#nullable disable
using System;
using System.Collections.Generic;
using AirLines.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace AirLines.Infrastructure.Data
{
    public partial class AirlinesContext : DbContext
    {
        public AirlinesContext()
        {
        }

        public AirlinesContext(DbContextOptions<AirlinesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AirPort> AirPorts { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passager> Passagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirPort>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.CheckIn).HasColumnType("datetime");

                entity.Property(e => e.CheckOut).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFlight");

                entity.HasOne(d => d.Passager)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PassagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPassager");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.ArriveConfirmed).HasColumnType("datetime");

                entity.Property(e => e.BoardingTime).HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DepartTime).HasColumnType("datetime");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.PriceChildren)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.HasOne(d => d.FromIdAirPortNavigation)
                    .WithMany(p => p.FlightFromIdAirPortNavigations)
                    .HasForeignKey(d => d.FromIdAirPort)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFlightFrom");

                entity.HasOne(d => d.ToIdAirPortNavigation)
                    .WithMany(p => p.FlightToIdAirPortNavigations)
                    .HasForeignKey(d => d.ToIdAirPort)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFlightTo");
            });

            modelBuilder.Entity<Passager>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}