using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Logic
{
    internal class PlayerLogic 
    {
        IRepository<Player> repo;

        public PlayerLogic(IRepository<Player> repo)
        {
            this.repo = repo;
        }

        public void Create(Player item)
        {
            if (item.PlayerPosition!="striker"|| item.PlayerPosition != "defender"|| item.PlayerPosition != "goalkeeper"|| item.PlayerPosition != "midfielder")
            {
                throw new ArgumentException("The position does not exists.");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Player Read(int id)
        {
            
            

            
            return this.repo.Read(id);
        }

        public IQueryable<Player> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Player item)
        {
            this.repo.Update(item);
        }
    }
}
