using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Models
{
    public class Player
    {

        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public int FootballTeamId { get; set; }
        public virtual FootballTeam FootballTeam { get; set; }
        public Player()
        {

        }

        public Player(string line)
        {
            string[] split = line.Split('#');
            this.PlayerId = int.Parse(split[0]);
            this.PlayerName = split[1];
            this.PlayerPosition = split[2];
            this.FootballTeamId = int.Parse(split[3]);

        }

    }
}
