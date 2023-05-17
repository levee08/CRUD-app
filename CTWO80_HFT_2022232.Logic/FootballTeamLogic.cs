using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using System;
using System.Linq;

namespace CTWO80_HFT_2022232.Logic
{
    public class FootballTeamLogic 
    {
        IRepository<FootballTeam> repo;

        public FootballTeamLogic(IRepository<FootballTeam> repo)
        {
            this.repo = repo;
        }

        public void Create(FootballTeam item)
        {
            if (item.FootballTeamName.Length<5)
            {
                throw new ArgumentException("Too short team name!");
            }
            if (item.CurrentPlacement>50)
            {
                throw new ArgumentException("Too big leauge.");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public FootballTeam Read(int id)
        {
            var team = this.repo.Read(id);
            if (team==null)
            {
                throw new ArgumentException("Team not found");
            }

            return team;
            
        }

        public IQueryable<FootballTeam> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(FootballTeam item)
        {
             this.repo.Update(item);
        }



    }
}
