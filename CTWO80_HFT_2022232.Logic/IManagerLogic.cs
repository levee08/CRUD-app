using CTWO80_HFT_2022232.Models;
using System.Linq;

namespace CTWO80_HFT_2022232.Logic
{
    internal interface IManagerLogic
    {
        void Create(Manager item);
        void Delete(int id);
        Manager Read(int id);
        IQueryable<Manager> ReadAll();
        void Update(Manager item);
    }
}