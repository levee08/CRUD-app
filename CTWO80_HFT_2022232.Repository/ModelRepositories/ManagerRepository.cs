using CTWO80_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Repository.ModelRepositories
{
    public class ManagerRepository :GenericRepository<Manager>, IRepository<Manager>
    {
        public ManagerRepository(LeaugeDbContext ctx) : base(ctx)
        {

        }

        public override Manager Read(int id)
        {
            return ctx.Managers.FirstOrDefault(t => t.ManagerId == id);
        }

        public override void Update(Manager item)
        {

            var old = Read(item.ManagerId);
            foreach (var prop in old.GetType().GetProperties())
            {

                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }

            }
            ctx.SaveChanges();

        }
    }
}
