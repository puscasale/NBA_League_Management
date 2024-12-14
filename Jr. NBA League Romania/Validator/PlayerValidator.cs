using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Validator;

public class PlayerValidator : IValidator<Player>
{
    public void Validate(Player entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Player cannot be null.");
    
        if (entity.Team == null)
            throw new ArgumentException("Team cannot be null for a player.");
    
        
        if (entity.Team.Id == null)
            throw new ArgumentException("Team must have a valid ID.");
    }

}