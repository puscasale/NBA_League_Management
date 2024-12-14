using Jr._NBA_League_Romania.Domain;
using System;
using System.Collections.Generic;
namespace Jr._NBA_League_Romania.Ui;

public class ConsoleApp  // Am schimbat numele clasei în ConsoleApp
{
    private readonly Service.Service service;

    public ConsoleApp(Service.Service service)
    {
        this.service = service;
    }

    public void PrintMenu()
    {
        System.Console.WriteLine("\t\t\tMENU\t\t\t");
        System.Console.WriteLine("1. Display players by team");
        System.Console.WriteLine("2. Display active players by team and match");
        System.Console.WriteLine("3. Display matches by date range");
        System.Console.WriteLine("4. Calculate match score");
        System.Console.WriteLine("0. EXIT");
    }

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            PrintMenu();
            string input = System.Console.ReadLine();
            switch (input)
            {
                case "1":
                    DisplayPlayersByTeam();
                    break;
                case "2":
                    DisplayActivePlayersByTeamAndMatch();
                    break;
                case "3":
                    DisplayMatchesByDateRange();
                    break;
                case "4":
                    CalculateMatchScore();
                    break;
                case "0":
                    System.Console.WriteLine("Exiting...");
                    exit = true;
                    break;
                default:
                    System.Console.WriteLine("Invalid input!");
                    break;
            }
        }
    }

    public void DisplayPlayersByTeam()
    {
        int teamId = GetIntInput("Enter team ID: ");
        var players = service.GetPlayersByTeam(teamId);
        foreach (var p in players)
        {
            System.Console.WriteLine(p);
        }
    }

    public void DisplayActivePlayersByTeamAndMatch()
    {
        int teamId = GetIntInput("Enter team ID: ");
        int matchId = GetIntInput("Enter match ID: ");
        var activePlayers = service.GetActivePlayersByTeamAndMatch(teamId, matchId);
        foreach (var player in activePlayers)
        {
            System.Console.WriteLine(player);
        }
    }

    public void DisplayMatchesByDateRange()
    {
        DateTime startDate = GetDateInput("Enter start date (yyyy-mm-dd): ");
        DateTime endDate = GetDateInput("Enter end date (yyyy-mm-dd): ");
        if (endDate < startDate)
        {
            System.Console.WriteLine("End date cannot be earlier than start date.");
        }
        else
        {
            var matches = service.GetMatchesByDateRange(startDate, endDate);
            foreach (var m in matches)
            {
                System.Console.WriteLine(m);
            }
        }
    }

    public void CalculateMatchScore()
    {
        int matchId = GetIntInput("Enter match ID: ");
        Func<ActivePlayer, int> scoringDelegate = ap => ap.NrPoints;
        int score = service.CalculateMatchScore(matchId, scoringDelegate);
        System.Console.WriteLine($"Match score: {score}");
    }

    
    
    // Helper method to get integer input
    private int GetIntInput(string prompt)
    {
        int result;
        while (true)
        {
            System.Console.WriteLine(prompt);
            if (int.TryParse(System.Console.ReadLine(), out result))
                break;
            System.Console.WriteLine("Invalid input! Please enter a valid integer.");
        }
        return result;
    }

    // Helper method to get DateTime input
    private DateTime GetDateInput(string prompt)
    {
        DateTime result;
        while (true)
        {
            System.Console.WriteLine(prompt);
            if (DateTime.TryParse(System.Console.ReadLine(), out result))
                break;
            System.Console.WriteLine("Invalid date format! Please enter a valid date (yyyy-mm-dd).");
        }
        return result;
    }
    
    

}
