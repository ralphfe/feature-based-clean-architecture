namespace TodoApp.Infrastructure.Options;

public class DbOptions
{
    public static string Options = nameof(DbOptions);
    
    public string ConnectionString { get; set; } = string.Empty;
}
