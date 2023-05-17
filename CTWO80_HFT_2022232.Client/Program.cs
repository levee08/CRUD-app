using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using CTWO80_HFT_2022232.Repository.ModelRepositories;
using System;
using System.Linq;

namespace CTWO80_HFT_2022232.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            LeaugeDbContext ctx =new LeaugeDbContext();

            ctx.Managers.Select(t => t.FootballTeams);
            ctx.FootballTeams.Select(t => t.TrophiesWon);
            ctx.Players.Select(t => t.FootballTeam);

            IRepository<Manager> repository = new ManagerRepository(new LeaugeDbContext());
            IRepository<FootballTeam> trepository = new FootballTeamRepository(new LeaugeDbContext());
            IRepository<Player> prepository = new PlayerRepository(new LeaugeDbContext());

            var items = repository.ReadAll().ToArray();
            var itemek = trepository.ReadAll().ToArray();
            var itemeks = prepository.ReadAll().ToArray();

            


        }
    }
}
