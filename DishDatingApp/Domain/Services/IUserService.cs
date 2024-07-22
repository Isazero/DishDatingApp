using DishDatingApp.Domain.Common;
using DishDatingApp.Domain.Dtos;

namespace DishDatingApp.Domain.Services;

public interface IUserService
{
    Task<UserDto> RegisterUser(RegisterUserDto registerUserDto, CancellationToken cancellationToken);
    Task<UserDto> UpdateUser(Guid id, UserDto updateUserDto, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<UserDto>> GetUsers(Gender gender, int minAge, int maxAge, IEnumerable<string>? dishes,
        CancellationToken cancellationToken);
    Task LikeDish(Guid fromUserId, Guid dishId, CancellationToken cancellationToken);
}
