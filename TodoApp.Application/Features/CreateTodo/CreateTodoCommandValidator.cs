namespace TodoApp.Application.Features.CreateTodo;

public class CreateTodoCommandValidator : IValidator<CreateTodoCommand>
{
    public Result<CreateTodoCommand> Validate(CreateTodoCommand command)
    {
        if (string.IsNullOrEmpty(command.Title))
            return new ValidationException("Title is required");
        
        if (command.Title.Length > 64)
            return new ValidationException("Title cannot be longer than 64 characters");
        
        if (command.Description?.Length > 256)
            return new ValidationException("Description cannot be longer than 256 characters");

        return command;
    }
}