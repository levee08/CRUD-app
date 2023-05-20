using CTWO80_HFT_2022232.Models;
using System.Collections.Generic;
using System.Linq;

namespace CTWO80_HFT_2022232.Logic
{
    public interface IFootballTeamLogic
    {
        IEnumerable<FootballTeam> BoldManagersTeamName();
        void Create(FootballTeam item);
        void Delete(int id);
        IEnumerable<KeyValuePair<string, int>> OldManagersTeamName();
        FootballTeam Read(int id);
        IQueryable<FootballTeam> ReadAll();
        IEnumerable<KeyValuePair<string, int>> TeamPlayersCount();
        void Update(FootballTeam item);
    }
}