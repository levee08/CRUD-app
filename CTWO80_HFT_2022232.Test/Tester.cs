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
            mockPlayer = new Mock<IRepository<Player>>();
            mockManager = new Mock<IRepository<Manager>>();


            var teams = new List<FootballTeam>()
            {
                 new FootballTeam("1#TeamA#10#2#1"),
                new FootballTeam("2#TeamB#30#8#2"),
                new FootballTeam("3#TeamC#40#7#3"),
                new FootballTeam("4#TeamD#50#6#4"),
                new FootballTeam("5#TeamE#60#10#5"),
            };


            var players = new List<Player>()
            {

                new Player("1#A#striker#1"),
                new Player("2#B#midfielder#2"),
                new Player("3#C#goalkeeper#3"),
                new Player("4#D#defender#4"),
                new Player("5#E#striker#5")
            };


            var managers = new List<Manager>()
          {
               new Manager("1#MA#10#true"),
                new Manager("2#MB#30#false"),
                new Manager("3#MC#40#false"),
                new Manager("4#MD#50#true"),
                new Manager("5#ME#60#true")
          };

            teams[0].Players = new List<Player>() { players[0] };
            teams[1].Players = new List<Player>() { players[1] };
            teams[2].Players = new List<Player>() { players[2] };
            teams[3].Players = new List<Player>() { players[3] };
            teams[4].Players = new List<Player>() { players[4] };

            managers[0].FootballTeams = new List<FootballTeam>() { teams[0] };
            managers[1].FootballTeams = new List<FootballTeam>() { teams[1] };
            managers[2].FootballTeams = new List<FootballTeam>() { teams[2] };
            managers[3].FootballTeams = new List<FootballTeam>() { teams[3] };
            managers[4].FootballTeams = new List<FootballTeam>() { teams[4] };

            teams[0].Manager = managers[0];
            teams[1].Manager = managers[1];
            teams[2].Manager = managers[2];
            teams[3].Manager = managers[3];
            teams[4].Manager = managers[4];

            players[0].FootballTeam = teams[0];
            players[1].FootballTeam = teams[1];
            players[2].FootballTeam = teams[2];
            players[3].FootballTeam = teams[3];
            players[4].FootballTeam = teams[4];
           


            mockFocirepo.Setup(x => x.ReadAll()).Returns(teams.AsQueryable());
            mockPlayer.Setup(x => x.ReadAll()).Returns(players.AsQueryable());
            mockManager.Setup(x => x.ReadAll()).Returns(managers.AsQueryable());

            logic = new FootballTeamLogic(mockFocirepo.Object);
            plogic = new PlayerLogic(mockPlayer.Object);
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
        [Test]
        public void InvalidManagerAge()
        {
            var manager = new Manager() { ManagerAge = 99 };
            try
            {
                mlogic.Create(manager);
            }
            catch 
            {

                
            }
            mockManager.Verify(r => r.Create(manager), Times.Never);
        }

        [Test]
        public void BoldManagersTeamNameTest()
        {
            var result = logic.BoldManagersTeamName();

            Assert.IsTrue(result.Any(x => x.FootballTeamName == "TeamA"));
            Assert.IsTrue(result.Any(x => x.FootballTeamName == "TeamD"));
            Assert.IsTrue(result.Any(x => x.FootballTeamName == "TeamE"));

            


        }

        [Test]
        public void PlayerCount()
        {
            var result = logic.TeamPlayersCount();

            var expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("TeamA",1),
                 new KeyValuePair<string, int>("TeamB",1),
                  new KeyValuePair<string, int>("TeamC",1),
                   new KeyValuePair<string, int>("TeamD",1),
                    new KeyValuePair<string, int>("TeamE",1)
            };

            Assert.That(result,Is.EqualTo(expected));
        }

        [Test]
        public void OldManagersTeamName()
        {
            var result = logic.OldManagersTeamName();

            var expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("TeamD",50),
                new KeyValuePair<string, int>("TeamE",60)

            };

            Assert.That(result,Is.EqualTo(expected));
        }
        [Test]
        public void PlayerStatByName()
        {
            var result = plogic.PlayerTrophiesAndPosition("A");

            var expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("striker",10)
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TrophiesByPositionTest()
        {
            var result = plogic.ThrophiesByPosition();

            var expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("striker",70),
                new KeyValuePair<string, int>("defender",50),
                new KeyValuePair<string, int>("goalkeeper",40),
                new KeyValuePair<string, int>("midfielder",30)

            };

            Assert.That(result, Is.EqualTo(expected));
        }





    }
}
