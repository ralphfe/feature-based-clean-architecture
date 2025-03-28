using System;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.UnitTests.Entities;

public class TodoTest
{
    [Fact]
    public void Create_WithValidData_ReturnsTodo()
    {
        // Arrange
        var title = "Test";
        var description = "Test";
        
        // Act
        var todo = Todo.Create(title, description);
        
        // Assert
        Assert.NotNull(todo);
    }
    
    [Fact]
    public void Create_WithNullTitle_ThrowsException()
    {
        // Arrange
        var title = string.Empty;
        var description = "Test";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Todo.Create(title, description));
    }
    
    [Fact]
    public void MarkAsCompleted_WhenCalled_SetsCompletedData()
    {
        // Arrange
        var title = "Test";
        var description = "Test";
        var todo = Todo.Create(title, description);
        
        // Act
        todo.MarkAsCompleted();
        
        // Assert
        Assert.True(todo.IsCompleted);
        Assert.NotNull(todo.CompletedDate);
    }
    
    [Fact]
    public void MarkAsCompleted_WithCompletedTodo_ThrowsException()
    {
        // Arrange
        var title = "Test";
        var description = "Test";
        var todo = Todo.Create(title, description);
        todo.MarkAsCompleted();
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => todo.MarkAsCompleted());
    }
}
