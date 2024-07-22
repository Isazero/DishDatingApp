using DishDatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DishDatingApp.Infrastructure.Database;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Like> Likes { get; set; }
}

//Add fluent api and specify Keys and Length of columns in tables