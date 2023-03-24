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
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<GateWithPenaltyTraining> GateWithPenaltyTrainings { get; set; }
        public DbSet<GateWithTimeTraining> GateWithTimeTrainings { get; set; }
        public DbSet<GateWithPenaltyPassageTraining> GateWithPenaltyPassageTrainings { get; set; }
        public DbSet<GateWithTimePassageTraining> GateWithTimePassageTrainings { get; set; }
        public DbSet<ParticipantOfTheTraining> ParticipantOfTheTrainings { get; set; }

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

            modelBuilder.Entity<Competition>()
                 .HasMany(x => x.Competitioners)
                 .WithOne(x => x.Competition).HasForeignKey(x => x.CompetitionId);

            modelBuilder.Entity<GateWithPenaltyPassage>()
                .HasOne(x => x.PenaltyGate)
                .WithMany(x => x.PenaltyPassages)
                .HasForeignKey(x => x.GateWihtPenaltyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GateWithTime>()
                .HasMany(x => x.TimePassages)
                .WithOne(x => x.TimeGate)
                .HasForeignKey(x => x.GateWihtTimeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Competitioner>()
                .HasMany(x => x.GateWithPenaltyPassages)
                .WithOne(x => x.Competitioner).HasForeignKey(x => x.CompetitionerId);

            modelBuilder.Entity<Competitioner>()
               .HasMany(x => x.GateWithTimePassages)
               .WithOne(x => x.Competitioner).HasForeignKey(x => x.CompetitionerId);

            modelBuilder.Entity<Coach>()
                 .HasMany(x => x.Sportsmens)
                 .WithOne(x => x.Coach)
                 .HasForeignKey(x => x.CoachId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GateWithPenaltyTraining>()
                .HasMany(x => x.PenaltyPassages)
                .WithOne(x => x.GateWithPenalty)
                .HasForeignKey(x => x.GateWihtPenaltyTrainingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GateWithTimeTraining>()
               .HasMany(x => x.TimePassages)
               .WithOne(x => x.TimeGate)
               .HasForeignKey(x => x.GateWithPenaltyTrainingId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ParticipantOfTheTraining>()
                .HasMany(x => x.GateWithPenaltyPassages)
                .WithOne(x => x.ParticipantOfTheTraining)
                .HasForeignKey(x => x.ParticipantOfTheTrainingId)
                .OnDelete(DeleteBehavior.NoAction);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
