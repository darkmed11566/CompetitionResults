using CompetitionResults.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options) { }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Sportsman> Sportsmens { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<GateWithTime> GateWithTimes { get; set; }
        public DbSet<GateWithPenalty> GateWithPenalties { get; set; }
        public DbSet<Competitioner> Competitioners { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<GateWithTimePassage> GateWithTimePassages { get; set; }
        public DbSet<GateWithPenaltyPassage> GateWithPenaltiePassages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>()
                 .HasMany(x => x.Sectors)
                 .WithOne(x => x.Track).HasForeignKey(x=>x.TrackId);

            modelBuilder.Entity<Sector>()
                .HasMany(x => x.GatesWithPenalty)
                .WithOne(x => x.Sector).HasForeignKey(x => x.SectorId);

            modelBuilder.Entity<Competition>()
               .HasMany(x => x.Tracks)
               .WithOne(x => x.Competition).HasForeignKey(x => x.CompetitionId);

            modelBuilder.Entity<Track>()
                 .HasMany(x => x.Gates)
                 .WithOne(x => x.Track).HasForeignKey(x => x.TrackId);

            modelBuilder.Entity<Sportsman>()
                 .HasMany(x => x.Competitioners)
                 .WithOne(x => x.Sportsman).HasForeignKey(x => x.SportsmanId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
