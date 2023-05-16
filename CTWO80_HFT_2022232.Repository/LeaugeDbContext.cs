using CTWO80_HFT_2022232.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Repository
{
    internal class LeaugeDbContext :DbContext
    {


        public DbSet<FootballTeam> FootballTeams { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Player> Players { get; set; }

        public LeaugeDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
