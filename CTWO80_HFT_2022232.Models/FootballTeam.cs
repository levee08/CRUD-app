using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Models
{
    public class FootballTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FootballTeamId { get; set; }
        [StringLength(100)]
        public string FootballTeamName { get; set; }

        public int TrophiesWon { get; set; }

        [Range(1,30)]
        public int CurrentPlacement { get; set; }

        public int ManagerId { get; set; }

        public virtual Manager Manager { get; set; }
        [NotMapped]
        public virtual ICollection<Player> Players { get; set; }
        public FootballTeam()
        {

        }
        public FootballTeam(string line)
        {
            string[] split = line.Split('#');
            this.FootballTeamId = int.Parse(split[0]);
            this.FootballTeamName =split[1];
            this.TrophiesWon = int.Parse(split[2]);
            this.CurrentPlacement = int.Parse(split[3]);
            this.ManagerId = int.Parse(split[4]);
        }


    }
}
