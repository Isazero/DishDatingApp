using DishDatingApp.Domain.Common;

namespace DishDatingApp.Domain.Dtos;

//Add validation through DataAnnotations
public record UserDto(string Email, string Name, Gender Gender, int Age, IEnumerable<string> FavoriteDishes);