using System;
using System.Collections;
using System.Collections.Generic;

namespace CTWO80_HFT_2022232.Models
{
    public class Manager
    {

        public int ManagerId { get; set; }
        public string ManagerName { get; set; }

        public int ManagerAge { get; set; }
        public bool IsBold { get; set; }


        public virtual FootballTeam FootballTeam { get; set; }

        public Manager()
        {
            FootballTeam = new FootballTeam();
        }
        public Manager(string line)
        {
            string[] split = line.Split('#');
            this.ManagerId = int.Parse(split[0]);
            this.ManagerName = split[1];
            this.ManagerAge = int.Parse(split[2]);
            this.IsBold = bool.Parse(split[3]);
            FootballTeam = new FootballTeam();

        }
    }
}
