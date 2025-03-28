namespace TodoApp.Application.Features;

public interface IValidator<T>
{
    Result<T> Validate(T entity);
}