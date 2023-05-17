using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Logic
{
    internal class ManagerLogic 
    {
        IRepository<Manager> repo;

        public ManagerLogic(IRepository<Manager> repo)
        {
            this.repo = repo;
        }

        public void Create(Manager item)
        {
            if (item.ManagerAge>80)
            {
                throw new ArgumentException("The Manager is too old!");
            }
            if (item.ManagerName.Length<3)
            {
                throw new ArgumentException("Too short name for a manager!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Manager Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Manager> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Manager item)
        {
            this.repo.Update(item);
        }

        public IEnumerable<T> ManagerWithTheMostThropie()
        {
           repo.
        }
    }
}
