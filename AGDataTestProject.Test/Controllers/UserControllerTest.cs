using AGDataTestProject.Interfaces;
using AGDataTestProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;


public interface ICacheWrapper
{
    object GetOrCreate(object key, Func<ICacheEntry, object> factory);
}

public class CacheWrapper : ICacheWrapper
{
    private readonly IMemoryCache _cache;

    public CacheWrapper(IMemoryCache cache)
    {
        _cache = cache;
    }

    public object GetOrCreate(object key, Func<ICacheEntry, object> factory)
    {
        return _cache.GetOrCreate(key, factory);
    }
}
public class UserControllerTests
{
    [Fact]
    public void AddUser_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        var cacheMock = new Mock<IMemoryCache>();
        var userServiceMock = new Mock<IUserService>();
        var controller = new UserController(cacheMock.Object, userServiceMock.Object);
        controller.ModelState.AddModelError("Name", "Name is required.");

        // Act
        var result = controller.AddUser(new User()) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void AddUser_ValidModel_ReturnsOkResult()
    {
        // Arrange
        var cacheMock = new Mock<IMemoryCache>();
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(repo => repo.AddUser(It.IsAny<User>())).Returns((true, "User added successfully."));
        var controller = new UserController(cacheMock.Object, userServiceMock.Object);

        // Act
        var result = controller.AddUser(new User()) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public void UpdateUser_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        var cacheMock = new Mock<IMemoryCache>();
        var userServiceMock = new Mock<IUserService>();
        var controller = new UserController(cacheMock.Object, userServiceMock.Object);
        controller.ModelState.AddModelError("Name", "Name is required.");

        // Act
        var result = controller.UpdateUser(new User()) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void UpdateUser_UserNotFound_ReturnsNotFound()
    {
        // Arrange
        var cacheMock = new Mock<IMemoryCache>();
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns((false, "User not found for update."));
        var controller = new UserController(cacheMock.Object, userServiceMock.Object);

        // Act
        var result = controller.UpdateUser(new User()) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public void UpdateUser_ValidModel_ReturnsOkResult()
    {
        // Arrange
        var cacheMock = new Mock<IMemoryCache>();
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns((true, "User updated successfully."));
        var controller = new UserController(cacheMock.Object, userServiceMock.Object);

        // Act
        var result = controller.UpdateUser(new User()) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public void DeleteUser_UserNotFound_ReturnsNotFound()
    {
        // Arrange
        var cacheMock = new Mock<IMemoryCache>();
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(repo => repo.DeleteUser(It.IsAny<string>())).Returns((false, "User not found."));
        var controller = new UserController(cacheMock.Object, userServiceMock.Object);

        // Act
        var result = controller.DeleteUser("UserName") as NotFoundObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public void DeleteUser_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var cacheMock = new Mock<IMemoryCache>();
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(repo => repo.DeleteUser(It.IsAny<string>())).Returns((true, "User deleted successfully."));
        var controller = new UserController(cacheMock.Object, userServiceMock.Object);

        // Act
        var result = controller.DeleteUser("UserName") as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}
