namespace DishDatingApp.Domain.Dtos;

public record LikeDishDto(Guid FromUserId, Guid ToUserId, Guid DishId);