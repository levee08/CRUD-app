using CTWO80_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Repository
{
    public class FootballTeamRepository : GenericRepository<FootballTeam>, IRepository<FootballTeam>
    {
        public FootballTeamRepository(LeaugeDbContext ctx ) :base(ctx)
        {

        }
        public override FootballTeam Read(int id)
        {
            return ctx.FootballTeams.FirstOrDefault(t => t.FootballTeamId == id);
        }

        public override void Update(FootballTeam item)
        {
            var old = Read(item.FootballTeamId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
