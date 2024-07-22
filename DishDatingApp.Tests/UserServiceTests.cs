using DishDatingApp.Application.Services;
using DishDatingApp.Domain.Common;
using DishDatingApp.Domain.Dtos;
using DishDatingApp.Domain.Services;
using DishDatingApp.Infrastructure.Database;
using DishDatingApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DishDatingApp.Tests;

public class UserServiceTests
{
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _context;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        var userRepository = new UserRepository(_context);
        var emailService = new EmailService();
        _userService = new UserService(userRepository, emailService);
    }

    [Fact]
    public async Task RegisterUser_Ok_ShouldAddUser()
    {
        // Arrange
        var registerUserDto = new RegisterUserDto("JohnDoe@testEmails.com", "SomeSuperHardPassword!!!", "John Doe",
            Gender.Male, 30, new List<string> { "Pizza", "Sushi" });

        // Act
        var user = await _userService.RegisterUser(registerUserDto, CancellationToken.None);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(registerUserDto.Email, user.Email);
        Assert.Equal(registerUserDto.Name, user.Name);
        Assert.Equal(registerUserDto.Gender, user.Gender);
        Assert.Equal(registerUserDto.Age, user.Age);
        Assert.Contains("Pizza", user.FavoriteDishes);
        Assert.Contains("Sushi", user.FavoriteDishes);
        //For password checking would need to check hashes and write hash function 
    }
    
    [Fact]
    public async Task RegisterUser_Fail_UserExists()
    {
        // Arrange
        var registerUserDto = new RegisterUserDto("JohnDoe@testEmails.com", "SomeSuperHardPassword!!!", "John Doe",
            Gender.Male, 30, new List<string> { "Pizza", "Sushi" });

        // Act && Assert
        await Assert.ThrowsAsync<Exception>(() => (_userService.RegisterUser(registerUserDto, CancellationToken.None)));
    }
    
    // Add tests for other methods in UserService. At least 1 successful case and 1 fail
    // Add tests for EmailService
    
    
}
