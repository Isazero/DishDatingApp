namespace DishDatingApp.Domain.Entities;

// Later will need to move Image Storage into AWS S3 or Azure BlobStorage
public class DishImages
{
    public Guid Id { get; set; }
    
    public Guid DishId { get; set; }
    
    public Dish Dish { get; set; }

    public byte[] Image { get; set; }
}