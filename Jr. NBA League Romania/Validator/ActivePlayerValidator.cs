using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Validator;

public class ActivePlayerValidator : IValidator<ActivePlayer>
{
    public void Validate(ActivePlayer entity)
    {
        if (entity.IdPlayer == null)
            throw new ArgumentException("Player ID must not be empty.");
        if (entity.IdMatch == null)
            throw new ArgumentException("Match ID must not be empty.");
        if (entity.NrPoints < 0)
            throw new ArgumentException("Points cannot be negative.");
        if (string.IsNullOrWhiteSpace(entity.Type) || 
            (entity.Type != "reserve" && entity.Type != "participant"))
            throw new ArgumentException("Type must be either 'reserve' or 'participant'.");
    }

}