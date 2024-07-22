using DishDatingApp.Domain.Common;
using DishDatingApp.Domain.Entities;

namespace DishDatingApp.Domain.Repositories;

public interface IUserRepository
{
    //Maybe need to add Predicate as Parameter so it will be 1 GetUser
    Task<User?> GetUser(Guid id, CancellationToken cancellationToken);
    Task<User?> GetUser(string email, CancellationToken cancellationToken);
    Task<IEnumerable<User>?> GetUsers(CancellationToken cancellationToken);
    Task AddUser(User user, CancellationToken cancellationToken);
    Task UpdateUser(User user, CancellationToken cancellationToken);
    
    Task<IEnumerable<User>> GetFilteredUsers(Gender gender, int minAge, int maxAge, IEnumerable<string>? dishes, CancellationToken cancellationToken);

    Task AddLikeToDish(Guid fromUserId, Guid dishId, CancellationToken cancellationToken);
}