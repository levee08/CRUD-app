using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CTWO80_HFT_2022232.Logic
{
    public class FootballTeamLogic : IFootballTeamLogic
    {
        IRepository<FootballTeam> repo;



        public FootballTeamLogic(IRepository<FootballTeam> repo)
        {
            this.repo = repo;
        }

        public void Create(FootballTeam item)
        {
            if (item.FootballTeamName.Length < 5)
            {
                throw new ArgumentException("Too short team name!");
            }
            if (item.CurrentPlacement > 50)
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
            if (team == null)
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


        //csapatok ahol kopasz a manager

        public IEnumerable<FootballTeam> BoldManagersTeamName()
        {
            return this.repo.ReadAll().Where(x => x.Manager.IsBold == true);

        }

        //egy csapatban hány játékos van
        public IEnumerable<KeyValuePair<string, int>> TeamPlayersCount()
        {
            return from x in this.repo.ReadAll()
                   select new KeyValuePair<string, int>(x.FootballTeamName, x.Players.Count());
        }

        //40 évnél idősebb managerek csapatainak neve és a manager életkora
        public IEnumerable<KeyValuePair<string, int>> OldManagersTeamName()
        {
            return from x in this.repo.ReadAll()
                   where x.Manager.ManagerAge > 40
                   select new KeyValuePair<string, int>(x.FootballTeamName, x.Manager.ManagerAge);
        }










    }




}
