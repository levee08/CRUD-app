using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CTWO80_HFT_2022232.Models
{
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }
        [StringLength(100)]
        public string ManagerName { get; set; }
        [Range(0,100)]
        public int ManagerAge { get; set; }
        public bool IsBold { get; set; }

        [NotMapped]
        public virtual ICollection<FootballTeam> FootballTeams { get; set; }

        public Manager()
        {
           
        }
        public Manager(string line)
        {
            string[] split = line.Split('#');
            this.ManagerId = int.Parse(split[0]);
            this.ManagerName = split[1];
            this.ManagerAge = int.Parse(split[2]);
            this.IsBold = bool.Parse(split[3]);
            

        }
    }
}
