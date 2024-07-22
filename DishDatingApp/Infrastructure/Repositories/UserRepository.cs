using DishDatingApp.Domain.Common;
using DishDatingApp.Domain.Entities;
using DishDatingApp.Domain.Repositories;
using DishDatingApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DishDatingApp.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetUser(Guid id, CancellationToken cancellationToken)
    {
        return await context.Users
            .Include(u => u.FavoriteDishes)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetUser(string email, CancellationToken cancellationToken)
    {
        return await context.Users
            .Include(u => u.FavoriteDishes)
            .FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
    }

    public async Task<IEnumerable<User>?> GetUsers(CancellationToken cancellationToken)
    {
        return await context.Users.Include(u => u.FavoriteDishes).ToListAsync(cancellationToken);
    }

    public async Task AddUser(User user, CancellationToken cancellationToken)
    {
        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            //Logging
            throw;
        }
    }

    public async Task UpdateUser(User user, CancellationToken cancellationToken)
    {
        try
        {

            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            //Logging
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetFilteredUsers(Gender gender, int minAge, int maxAge,
        IEnumerable<string>? dishes, CancellationToken cancellationToken)
    {
        var query = context.Users.Include(u => u.FavoriteDishes).AsQueryable();

        var dishesList = dishes.ToList();
        if (dishesList.Any())
        {
            query = query.Where(u => u.FavoriteDishes.Any(d => dishesList.Contains(d.Name)));
        }
        query = query.Where(u => u.Gender == gender);
        query = query.Where(u => u.Age >= minAge);
        query = query.Where(u => u.Age <= maxAge);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddLikeToDish(Guid fromUserId, Guid dishId, CancellationToken cancellationToken)
    {
        await context.Likes.AddAsync(new Like { DishId = dishId, FromUserId = fromUserId }, cancellationToken);
    }
}