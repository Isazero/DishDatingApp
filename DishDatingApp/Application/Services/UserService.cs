using System.Security.Cryptography;
using DishDatingApp.Domain.Common;
using DishDatingApp.Domain.Dtos;
using DishDatingApp.Domain.Entities;
using DishDatingApp.Domain.Repositories;
using DishDatingApp.Domain.Services;

namespace DishDatingApp.Application.Services;

//Will need to add custom exceptions and logging of errors
public class UserService(IUserRepository userRepository, IEmailService emailService) : IUserService
{

    public async Task<UserDto> RegisterUser(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
    {
        var userDb = await userRepository.GetUser(registerUserDto.Email, cancellationToken);
        if (userDb is not null)
        {
            throw new Exception("User with this email already exists.");
        }

        var (hashedPassword, salt) = HashPassword(registerUserDto.Password);

        var user = new User
        {
            Name = registerUserDto.Name,
            Gender = registerUserDto.Gender,
            Age = registerUserDto.Age,
            FavoriteDishes = registerUserDto.FavoriteDishes.Select(x => new Dish { Name = x }).ToList(),
            Email = registerUserDto.Email,
            PasswordHash = hashedPassword,
            Salt = salt,
            EmailConfirmed = false
        };

        try
        {
            await userRepository.AddUser(user, cancellationToken);
            var confirmationToken = GenerateEmailConfirmationToken();
            await emailService.SendEmailConfirmation(registerUserDto.Email, confirmationToken, cancellationToken);

        }
        catch (Exception e)
        {
            //Logging
            //create SendingEmailFailedException 
            throw;
        }

        return ToUserDto(user);
    }

    public async Task<UserDto> UpdateUser(Guid id, UserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUser(id, cancellationToken);
        if (user == null) throw new Exception("User not found");
        
        user.Name = updateUserDto.Name;
        user.Gender = updateUserDto.Gender;
        user.Age = updateUserDto.Age;
        user.FavoriteDishes = updateUserDto.FavoriteDishes.Select(d => new Dish { Name = d }).ToList();
        
        await userRepository.UpdateUser(user, cancellationToken);

        return ToUserDto(user); 
    }


    public async Task<IReadOnlyCollection<UserDto>> GetUsers(Gender gender, int minAge, int maxAge,
        IEnumerable<string>? dishes, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetFilteredUsers(gender, minAge, maxAge, dishes, cancellationToken);

        return users
            .Select(ToUserDto)
            .ToList();
    }

    public async Task LikeDish(Guid fromUserId, Guid dishId,CancellationToken cancellationToken)
    {
        await userRepository.AddLikeToDish(fromUserId, dishId, cancellationToken);
    }
    
    private UserDto ToUserDto(User user)
    {
        return new UserDto(user.Email, user.Name, user.Gender,
            user.Age, user.FavoriteDishes.Select(d => d.Name).ToList());
    }
    
    private (string hashedPassword, string salt) HashPassword(string password)
    {
        //Placeholder. Will spend to much time on creating logic for hashing 
        return (password, "someSalt");
    }
    
    public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        // //Placeholder. Will spend to much time on creating logic for checking hashes 
        var saltBytes = Convert.FromBase64String(storedSalt);
        var hashed = enteredPassword;
        return hashed == storedHash;
    }
    
    private string GenerateEmailConfirmationToken()
    {       
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
