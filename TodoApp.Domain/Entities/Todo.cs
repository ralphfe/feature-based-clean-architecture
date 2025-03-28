namespace TodoApp.Domain.Entities;

public class Todo
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? CompletedDate { get; set; }
    
    private Todo()
    {
    }
    
    public static Todo Create(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null or whitespace", nameof(title));
        }
        
        if (title.Length > 64)
        {
            throw new ArgumentException("Title cannot be longer than 64 characters", nameof(title));
        }
        
        if (description?.Length > 256)
        {
            throw new ArgumentException("Description cannot be longer than 256 characters", nameof(description));
        }
        
        return new Todo
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedDate = DateTimeOffset.UtcNow,
        };
    }
    
    public void MarkAsCompleted()
    {
        if (this.IsCompleted)
        {
            throw new InvalidOperationException("Todo is already completed");
        }
        
        IsCompleted = true;
        CompletedDate = DateTimeOffset.UtcNow;
    }
}
