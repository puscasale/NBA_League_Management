using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Validator;

public class StudentValidator : IValidator<Student>
{
    public void Validate(Student entity)
    {
        if (entity.Id == null)
            throw new ArgumentException("ID must be greater than 0.");
        if (string.IsNullOrWhiteSpace(entity.Name))
            throw new ArgumentException("Name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(entity.School))
            throw new ArgumentException("School cannot be null or empty.");
    }
}