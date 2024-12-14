using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Validator;

public class MatchValidator : IValidator<Match>
{
    public void Validate(Match entity)
    {
        if (entity.Team1 == null)
            throw new ArgumentException("Team1 cannot be null.");
        if (entity.Team2 == null)
            throw new ArgumentException("Team2 cannot be null.");
        if (entity.Team1.Id == entity.Team2.Id)
            throw new ArgumentException("Teams in a match must be different.");
        if (entity.Date < DateTime.Now)
            throw new ArgumentException("Match date cannot be in the past.");
    }
}