using Jr._NBA_League_Romania.Domain;
using Jr._NBA_League_Romania.Repository;

namespace Jr._NBA_League_Romania.Service;

public class Service
{
    private readonly PlayerBD playerRepository;
    private readonly ActivePlayerBD activePlayerRepository;
    private readonly MatchBD matchRepository;
    private readonly TeamBD teamRepository;

    public Service(PlayerBD playerRepository, ActivePlayerBD activePlayerRepository, MatchBD matchRepository, TeamBD teamRepository)
    {
        this.playerRepository = playerRepository;
        this.activePlayerRepository = activePlayerRepository;
        this.matchRepository = matchRepository;
        this.teamRepository = teamRepository;
    }
    
    // 1. Display all players of a given team
    public IEnumerable<Player> GetPlayersByTeam(int teamId)
    {
        var players = playerRepository.FindAll();
        return players.Where(player => player.Team.Id == teamId);
    }

    // 2. Display all active players of a team in a specific match
    public IEnumerable<ActivePlayer> GetActivePlayersByTeamAndMatch(int teamId, int matchId)
    {
        var activePlayers = activePlayerRepository.FindAll();
        return activePlayers.Where(ap => ap.IdMatch == matchId);
    }

    // 3. Display all matches within a specific date range
    public IEnumerable<Match> GetMatchesByDateRange(DateTime startDate, DateTime endDate)
    {
        var matches = matchRepository.FindAll();
        return matches.Where(match => match.Date >= startDate && match.Date <= endDate);
    }

    // 4. Calculate and display the score for a specific match using Generics, LINQ, and Delegate
    public int CalculateMatchScore(int matchId, Func<ActivePlayer, int> scoringDelegate)
    {
        var activePlayers = activePlayerRepository.FindAll().Where(ap => ap.IdMatch == matchId);
        return activePlayers.Sum(scoringDelegate);
    }
}
