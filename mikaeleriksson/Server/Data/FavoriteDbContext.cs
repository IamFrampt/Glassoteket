using Microsoft.EntityFrameworkCore;
using mikaeleriksson.Shared;

namespace mikaeleriksson.Server.Data;

public class FavoriteDbContext : DbContext
{
    public DbSet<Favorite> FavoriteIceCreams { get; set; }

    public DbSet<UserModel> Users { get; set; }
    public FavoriteDbContext(DbContextOptions options) : base(options)
    {

    }
}
