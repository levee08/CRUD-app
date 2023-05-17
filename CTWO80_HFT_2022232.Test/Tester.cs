﻿using CTWO80_HFT_2022232.Logic;
using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTWO80_HFT_2022232.Test
{
    [TestFixture]
    public class Tester
    {
        FootballTeamLogic logic;
        Mock<IRepository<FootballTeam>> mockFocirepo;

        [SetUp]
        public void Init()
        {

            mockFocirepo = new Mock<IRepository<FootballTeam>>();
            mockFocirepo.Setup(t => t.ReadAll()).Returns(new List<FootballTeam>()
            {
                new FootballTeam("1#TeamA#10#2#1"),
                new FootballTeam("2#TeamB#10#8#2"),
                new FootballTeam("3#TeamC#10#7#3"),
                new FootballTeam("4#TeamD#10#6#4"),
                new FootballTeam("5#TeamE#10#10#5"),
            }.AsQueryable());
            logic = new FootballTeamLogic(mockFocirepo.Object);



            // this.FootballTeamId = int.Parse(split[0]);
            //this.FootballTeamName = split[1];
            //this.TrophiesWon = int.Parse(split[2]);
            //this.CurrentPlacement = int.Parse(split[3]);
            //this.ManagerId = int.Parse(split[4]);
        }
    }
}
