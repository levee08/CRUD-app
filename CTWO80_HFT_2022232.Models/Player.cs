using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CTWO80_HFT_2022232.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [StringLength(100)]
        public string PlayerName { get; set; }
        [StringLength(100)]
        public string PlayerPosition { get; set; }
        public int FootballTeamId { get; set; }
        [JsonIgnore]
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
