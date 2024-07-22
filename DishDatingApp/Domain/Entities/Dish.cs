namespace DishDatingApp.Domain.Entities;

//Maybe need to add Specification of Cuisine like: Italian or Japanese.
//Because I might know couple of dishes and not know super interesting dish that exists there
public class Dish
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}
