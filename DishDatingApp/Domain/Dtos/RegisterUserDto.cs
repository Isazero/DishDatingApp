using DishDatingApp.Domain.Common;

namespace DishDatingApp.Domain.Dtos;

//Add validation through DataAnnotations
public record RegisterUserDto(string Email, string Password, string Name, Gender Gender, int Age, IEnumerable<string> FavoriteDishes);