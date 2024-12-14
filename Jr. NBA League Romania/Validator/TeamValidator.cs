using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Validator;

public class TeamValidator : IValidator<Team>
{
    public void Validate(Team entity)
    {
        if (entity.Id == null)
            throw new ArgumentException("Id must be greater than 0.");
        if (string.IsNullOrWhiteSpace(entity.Name))
            throw new ArgumentException("Name cannot be null or empty.");
    }
}
