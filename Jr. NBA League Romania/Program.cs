using Jr._NBA_League_Romania.Domain;
using Jr._NBA_League_Romania.Repository;
using Jr._NBA_League_Romania.Service;
using Jr._NBA_League_Romania.Validator;
using System;
using Jr._NBA_League_Romania.Ui;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost; Port=5432; Database=NBA; Username=postgres; Password=alesefa;";

        // Instantiate the repositories
        PlayerBD playerRepository = new PlayerBD(connectionString);
        ActivePlayerBD activePlayerRepository = new ActivePlayerBD(connectionString);
        MatchBD matchRepository = new MatchBD(connectionString);
        TeamBD teamRepository = new TeamBD(connectionString);

        // Instantiate the validators
        IValidator<Player> playerValidator = new PlayerValidator();
        IValidator<ActivePlayer> activePlayerValidator = new ActivePlayerValidator();
        IValidator<Match> matchValidator = new MatchValidator();
        IValidator<Team> teamValidator = new TeamValidator();

        // Instantiate the service
        Service service = new Service(playerRepository, activePlayerRepository, matchRepository, teamRepository);
        
        ConsoleApp consoleApp = new ConsoleApp(service);
        consoleApp.Run();
    }
}