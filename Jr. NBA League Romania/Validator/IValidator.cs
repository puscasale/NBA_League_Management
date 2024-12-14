namespace Jr._NBA_League_Romania.Validator;

public interface IValidator<T>
{
    void Validate(T entity);
}