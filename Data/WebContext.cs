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
            modelBuilder.Entity<Competition>()
                .HasMany(x => x.CompetitionTracks)
                .WithOne(x => x.NameOfCompetition);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
