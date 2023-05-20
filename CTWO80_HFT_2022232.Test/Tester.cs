using CTWO80_HFT_2022232.Logic;
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

        PlayerLogic plogic;
        Mock <IRepository<Player>> mockPlayer;

        ManagerLogic mlogic;
        Mock<IRepository<Manager>> mockManager;

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

            mockPlayer = new Mock<IRepository<Player>>();
            mockPlayer.Setup(t => t.ReadAll()).Returns(new List<Player>()
            {
                new Player("1#A#striker#1"),
                new Player("2#B#striker#2"),
                new Player("3#C#striker#3"),
                new Player("4#D#striker#4"),
                new Player("5#E#striker#5"),
            }.AsQueryable());
            plogic = new PlayerLogic(mockPlayer.Object);

            mockManager =new Mock<IRepository<Manager>>();
            mockManager.Setup(t => t.ReadAll()).Returns(new List<Manager>()
            {
                new Manager("1#MA#10#true"),
                new Manager("2#MB#30#false"),
                new Manager("3#MC#40#false"),
                new Manager("4#MD#500#true"),
                new Manager("5#ME#600#true"),

            }.AsQueryable());
            mlogic =new ManagerLogic(mockManager.Object);
            
        }


       

        [Test]
        public void CreateTesterValidValue()
        {
            var team = new FootballTeam() { FootballTeamName = "barcelona" };

            logic.Create(team);

            mockFocirepo.Verify(r=>r.Create(team),Times.Once);

        }

        [Test]
        public void CreateTesterInValidValue()
        {
            var team = new FootballTeam() { FootballTeamName = "AC" };

            try
            {
                logic.Create(team);
            }
            catch 
            {

                
            }
           

            mockFocirepo.Verify(r => r.Create(team), Times.Never);

        }

        [Test]
        public void CreatePlayerTest()
        {

            var player = new Player() { PlayerPosition = "striker" };
            plogic.Create(player);
            mockPlayer.Verify(r => r.Create(player), Times.Once);

        }

        [Test]
        public void InValidCreatePlayerTest()
        {
            var player = new Player() { PlayerPosition = "kispad" };
            try
            {
                plogic.Create(player);
            }
            catch
            {

            }
            
            mockPlayer.Verify(r=>r.Create(player), Times.Never);
        }

      

    }
}
