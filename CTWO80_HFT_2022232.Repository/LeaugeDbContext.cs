using CTWO80_HFT_2022232.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Repository
{
    public class LeaugeDbContext :DbContext
    {


        public DbSet<FootballTeam> FootballTeams { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Player> Players { get; set; }

        public LeaugeDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;MultipleActiveResultSets=true");

                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FootballTeam>()
                 .HasOne(ft => ft.Manager)
                 .WithMany(m => m.FootballTeams)
                 .HasForeignKey(ft => ft.ManagerId)
                 .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<FootballTeam>()
                .HasMany(team => team.Players)
                .WithOne(player => player.FootballTeam)
                .HasForeignKey(player => player.FootballTeamId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<FootballTeam>().HasData(new FootballTeam[]
            {
                new FootballTeam("1#Barcelona#42#1#1"),
                new FootballTeam("2#Madrid#0#20#5"),
                new FootballTeam("3#Celtic#10#3#2"),
                new FootballTeam("4#Inter#15#4#3"),
                new FootballTeam("5#Arsenal#20#7#7"),
                new FootballTeam("6#United#1#5#6"),
                new FootballTeam("7#Milan#28#6#4")

            });

            modelBuilder.Entity<Manager>().HasData(new Manager[]
            {
                new Manager("1#Arnold#30#true"),
                new Manager("2#Béla#33#false"),
                new Manager("3#Csongor#34#true"),
                new Manager("4#Dániel#27#true"),
                new Manager("5#Ottó#42#false"),
                new Manager("6#László#50#false"),
                new Manager("7#Noel#61#false"),


            });

           
            modelBuilder.Entity<Player>().HasData(new Player[]
            {

                new Player("1#Messi#striker#1"),
                new Player("2#Iniesta#midfielder#1"),
                new Player("3#Ronaldo#striker#2"),
                new Player("4#Ramos#defender#2"),
                new Player("5#Leao#striker#3"),
                new Player("6#Maignan#goalkeeper#3"),
                new Player("7#Modric#midfielder#4"),
                new Player("8#Kroos#midfielder#4"),
                new Player("9#Xavi#defender#5"),
                new Player("10#Kaka#striker#5"),
                new Player("11#Haaland#striker#6"),
                new Player("12#Szoboszlai#midfielder#6"),
                new Player("13#Valdes#goalkeeper#7"),
                new Player("14#Cassilas#goalkeeper#7"),

            });



        }

        
    }
}
