using CTWO80_HFT_2022232.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Repository.ModelRepositories
{
    public class PlayerRepository : GenericRepository<Player>, IRepository<Player>
    {
        public PlayerRepository(LeaugeDbContext ctx):base(ctx)
        {

        }
        public override Player Read(int id)
        {
            return ctx.Players.FirstOrDefault(t => t.PlayerId == id);

        }

        public override void Update(Player item)
        {

            var old = Read(item.PlayerId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();

        }
    }
}
