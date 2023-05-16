using CTWO80_HFT_2022232.Repository;
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

            Console.ReadKey();


        }
    }
}
