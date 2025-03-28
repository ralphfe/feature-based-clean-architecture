using TodoApp.Application.Features;

namespace TodoApp.Application.UnitTests.Features;

public class ResultTest
{
    [Fact]
    public void Success_WhenCalled_ReturnsSuccessResult()
    {
        // Arrange
        var value = "Test";
        
        // Act
        var result = Result<string>.Success(value);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Failure_WhenCalled_ReturnsFailureResult()
    {
        // Arrange
        var error = new Exception("Test error");
        
        // Act
        var result = Result<string>.Failure(error);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }
}
