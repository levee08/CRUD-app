using ConsoleTools;
using CTWO80_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CTWO80_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "FootballTeam")
            {
                Console.Write("Enter FootballTeam Name: ");
                string name = Console.ReadLine();
                rest.Post(new FootballTeam() { FootballTeamName = name }, "FootballTeam");
            }

            if (entity == "Manager")
            {
                Console.Write("Enter Manager Name: ");
                string name = Console.ReadLine();
                rest.Post(new Manager() { ManagerName = name }, "Manager");
            }

            if (entity == "Player")
            {
                Console.Write("Enter Player Name: ");
                string name = Console.ReadLine();
                rest.Post(new Player() { PlayerName = name }, "Player");
            }

        }
        static void List(string entity)
        {
            if (entity == "FootballTeam")
            {
                List<FootballTeam> footballTeams = rest.Get<FootballTeam>("FootballTeam");
                foreach (var item in footballTeams)
                {
                    Console.WriteLine(item.FootballTeamId + ": " + item.FootballTeamName);
                }
                Console.ReadLine();
            }


            if (entity == "Manager")
            {
                List<Manager> managers = rest.Get<Manager>("Manager");
                foreach (var item in managers)
                {
                    Console.WriteLine(item.ManagerId + ": " + item.ManagerName);
                }
                Console.ReadLine();
            }


            if (entity == "Player")
            {
                List<Player> players = rest.Get<Player>("Player");
                foreach (var item in players)
                {
                    Console.WriteLine(item.PlayerId + ": " + item.PlayerName);
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
