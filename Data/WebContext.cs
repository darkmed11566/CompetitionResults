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
    }
}
