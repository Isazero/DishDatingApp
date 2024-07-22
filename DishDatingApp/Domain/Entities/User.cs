using DishDatingApp.Domain.Common;

namespace DishDatingApp.Domain.Entities;

//Would inherit from Identity User and work with Microsoft User Manager
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public IEnumerable<Dish> FavoriteDishes { get; set; }

    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
}
