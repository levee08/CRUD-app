using CTWO80_HFT_2022232.Models;
using System.Collections.Generic;
using System.Linq;

namespace CTWO80_HFT_2022232.Logic
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        IEnumerable<KeyValuePair<string, int>> PlayerTrophiesAndPosition(string name);
        Player Read(int id);
        IQueryable<Player> ReadAll();
        IEnumerable<KeyValuePair<string, int>> ThrophiesByPosition();
        void Update(Player item);
    }
}