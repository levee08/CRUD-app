using ConsoleTools;
using CTWO80_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Web.Http.Results;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CTWO80_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;

        static void BoldMangersTeamName()
        {
            var teamnames = rest.Get<FootballTeam>("/noncrud/BoldManagersTeamName");
            foreach (var item in teamnames)
            {
                Console.WriteLine(item.FootballTeamName);
            }
            Console.ReadLine();
        }

        static void TeamPlayersCount()
        {
            
            var data =rest.Get<KeyValuePair<string,int>>("/noncrud/TeamPlayersCount");
            
            
                foreach (var item in data)
                {
                    Console.WriteLine(item.Key + " " + item.Value);
                }
                Console.ReadLine();
            
           
        }
        static void OldMangersTeamName()
        {
            var data = rest.Get<KeyValuePair<string, int>>("/noncrud/OldManagersTeamName");
            foreach (var item in data)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            Console.ReadLine();
        }
        static void PlayerStatByName()
        {
            Console.Write("Enter a player's name:");
            string name = Console.ReadLine();
            var datas = rest.Get<KeyValuePair<string, int>>("/PlayerNonCrud/PlayerTrophiesAndPosition/"+name);
            foreach (var item in datas)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            Console.ReadLine();

        }
        static void GroupByPosition()
        {
            
            var datas = rest.Get<KeyValuePair<string, int>>("/PlayerNonCrud/ThrophiesByPosition");
            foreach (var item in datas)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            Console.ReadLine();

        }

        static void Create(string entity)
        {
            if (entity == "FootballTeam")
            {
                Console.Write("Enter FootballTeam Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter How Many Throphies did your team win: ");
                int trophies = int.Parse(Console.ReadLine());
                Console.Write("Enter your team current placement:");
                int placement =int.Parse(Console.ReadLine());
                Console.Write("Enter your team's managerid:");
                int managerid = int.Parse(Console.ReadLine());
                rest.Post(new FootballTeam() { FootballTeamName = name ,TrophiesWon=trophies,CurrentPlacement=placement,ManagerId=managerid}, "FootballTeam");
            }

            if (entity == "Manager")
            {

                 Console.Write("Enter Manager Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter your manager's age:");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Enter if your manager is bold (true/false):");
                bool bold = bool.Parse(Console.ReadLine());



                rest.Post(new Manager() { ManagerName = name ,ManagerAge=age,IsBold=bold}, "Manager");
            }

            if (entity == "Player")
            {
     
                Console.Write("Enter Player Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Player Position(striker/defender/midfielder/goalkeeper):");
                string position = Console.ReadLine();
                Console.Write("Enter footballteamid:");
                int ftid=int.Parse(Console.ReadLine());
                rest.Post(new Player() { PlayerName = name,PlayerPosition=position,FootballTeamId=ftid }, "Player");
            }

        }
        


        static void List(string entity)
        {
            if (entity == "FootballTeam")
            {
                List<FootballTeam> footballTeams = rest.Get<FootballTeam>("FootballTeam");
               
                Console.WriteLine("{0,-5} {1,-15} {2,-10} {3,-10} {4,-10}", "ID", "Team Name", "Trophies", "Placement", "Manager ID");
                Console.WriteLine("-----------------------------------------------------------");

                
                foreach (var team in footballTeams)
                {
                    Console.WriteLine("{0,-5} {1,-15} {2,-10} {3,-10} {4,-10}", team.FootballTeamId, team.FootballTeamName, team.TrophiesWon, team.CurrentPlacement, team.ManagerId);
                }
                Console.ReadLine();
            }


            if (entity == "Manager")
            {
                List<Manager> managers = rest.Get<Manager>("Manager");
                
                Console.WriteLine("{0,-10} {1,-15} {2,-10} {3,-10}", "ID", "Manager Name", "Age", "Is Bold");
                Console.WriteLine("----------------------------------------");

                foreach (var manager in managers)
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-10} {3,-10}", manager.ManagerId, manager.ManagerName, manager.ManagerAge, manager.IsBold);
                }
                Console.ReadLine();
            }


            if (entity == "Player")
            {
                List<Player> players = rest.Get<Player>("Player");
               
                Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10}", "ID", "Player Name", "Position", "Team ID");
                Console.WriteLine("------------------------------------------");

                
                foreach (var player in players)
                {
                    Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10}", player.PlayerId, player.PlayerName, player.PlayerPosition, player.FootballTeamId);
                }
                Console.ReadLine();
            }


        }
        static void Update(string entity)
        {
            if (entity == "FootballTeam")
            {
                Console.Write("Enter FootballTeam's id to update: ");
                int id = int.Parse(Console.ReadLine());
                FootballTeam one = rest.Get<FootballTeam>(id, "FootballTeam");
                Console.Write($"New name [old: {one.FootballTeamName}]: ");
                string name = Console.ReadLine();
                one.FootballTeamName = name;
                Console.Write($"New placement [old: {one.CurrentPlacement}]: ");
                int placement = int.Parse(Console.ReadLine());
                one.CurrentPlacement = placement;
                Console.Write($"New managerid [old: {one.ManagerId}]: ");
                int managerid = int.Parse(Console.ReadLine());
                one.ManagerId = managerid;
                Console.Write($"New throphieswon [old: {one.TrophiesWon}]: ");
                int trophie = int.Parse(Console.ReadLine());
                one.TrophiesWon = trophie;
                
                rest.Put(one, "FootballTeam");
            }
            if (entity == "Manager")
            {
                Console.Write("Enter Manager's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Manager one = rest.Get<Manager>(id, "Manager");
                Console.Write($"New name [old: {one.ManagerName}]: ");
                string name = Console.ReadLine();
                one.ManagerName = name;
                Console.Write($"New age [old: {one.ManagerAge}]: ");
                int age = int.Parse(Console.ReadLine());
                one.ManagerAge = age;
                Console.Write($"New bold [old: {one.IsBold}]: ");
                bool bold = bool.Parse(Console.ReadLine());
                one.IsBold = bold;
                rest.Put(one, "Manager");
            }
            if (entity == "Player")
            {
                Console.Write("Enter Player's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Player one = rest.Get<Player>(id, "Player");
                Console.Write($"New name [old: {one.PlayerName}]: ");
                string name = Console.ReadLine();
                one.PlayerName = name;
                Console.Write($"New position [old: {one.PlayerPosition}]: ");
                string position = Console.ReadLine();
                one.PlayerPosition = position;
                Console.Write($"New footballteamid [old: {one.FootballTeamId}]: ");
                int footballTeamId = int.Parse(Console.ReadLine());
                one.FootballTeamId = footballTeamId;
                rest.Put(one, "Player");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "FootballTeam")
            {
                Console.Write("Enter FootballTeam's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "FootballTeam");
            }
            if (entity == "Manager")
            {
                Console.Write("Enter Manager's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Manager");
            }
            if (entity == "Player")
            {
                Console.Write("Enter Player's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Player");
            }
        }
        static void Main(string[] args)
        {

            rest = new RestService("http://localhost:29829/", "FootballTeam");

            var FootballteamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("FootballTeam"))
                .Add("Create", () => Create("FootballTeam"))
                .Add("Delete", () => Delete("FootballTeam"))
                .Add("Update", () => Update("FootballTeam"))
                .Add("Teams with player count",()=>TeamPlayersCount())
                .Add("Team names with managers older than 40",()=>OldMangersTeamName())
                .Add("Team names with bold manager",()=> BoldMangersTeamName())
                .Add("Exit", ConsoleMenu.Close);

            var managerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Manager"))
                .Add("Create", () => Create("Manager"))
                .Add("Delete", () => Delete("Manager"))
                .Add("Update", () => Update("Manager"))
                .Add("Exit", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))
                .Add("Group By Position",()=> GroupByPosition())
                .Add("Player stat by Name",()=> PlayerStatByName())
                .Add("Exit", ConsoleMenu.Close);

         


            var menu = new ConsoleMenu(args, level: 0)
                
                .Add("FootballTeam", () => FootballteamSubMenu.Show())
                .Add("Manager", () => managerSubMenu.Show())
                .Add("Player", () => playerSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

            


        }
    }
}
