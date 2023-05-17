using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using System.Collections.Generic;
using System.Linq;

namespace CTWO80_HFT_2022232.Logic
{
    public interface IFootballTeamLogic
    {

        
        void Create(FootballTeam item);
        void Delete(int id);
        FootballTeam Read(int id);
        IQueryable<FootballTeam> ReadAll();
        IEnumerable<TeamPlayers> TeamsWithPlayerCounts();
        void Update(FootballTeam item);
    }
}