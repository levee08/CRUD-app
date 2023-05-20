using ConsoleTools;
using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using CTWO80_HFT_2022232.Repository.ModelRepositories;
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
                rest.Post(new FootballTeam() { FootballTeamName = name }, "actor");
            }
        }
        static void List(string entity)
        {
            if (entity == "FootballTeam")
            {
                List<FootballTeam> actors = rest.Get<FootballTeam>("FootballTeam");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.FootballTeamId + ": " + item.FootballTeamName);
                }
            }
            Console.ReadLine();
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
        }
        static void Delete(string entity)
        {
            if (entity == "FootballTeam")
            {
                Console.Write("Enter FootballTeam's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "FootballTeam");
            }
        }
        static void Main(string[] args)
        {

            rest = new RestService("http://localhost:53910/", "FootballTeam");

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
