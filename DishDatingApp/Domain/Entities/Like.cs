namespace DishDatingApp.Domain.Entities;

// If need to count and display the total number of likes
// Then we will have to make a separate table TotalLikes(DishId, TotalLikesCount)
// Since running Count will be too long and expensive
// Each time a like is added to the likes table, an increment will occur in TotalLikes
public class Like
{
    public Guid Id { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public Guid DishId { get; set; }
    public Dish Dish { get; set; }
}