using ElectricalProspectingProfiling.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElectricalProspectingProfiling.Database.context
{
    public class MyDBContext:DbContext
    {
        private readonly string connectionString = "Server=DESKTOP-N2FEHOR\\MSSQLSERVER01; Database=GeodeticSurvey; Trusted_Connection=True; TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Square> Squares { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Picket> Picket { get; set; }
        public DbSet<Measurement> Measurement { get; set; }
        public DbSet<GeologicalData> GeologicalData { get; set; }
        public DbSet<Geodesist> Geodesist { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CoordinatsSquare> CoordinatsSquare { get; set; }
        public DbSet<CoordinatsProfile> CoordinatsProfile { get; set; }
        public DbSet<Model.Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
             .HasOne(p => p.Square)
             .WithMany()
             .HasForeignKey(p => p.ПлощадьID)
             .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.CoordinatsProfile)
                .WithMany()
                .HasForeignKey(p => p.КоординатыID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
