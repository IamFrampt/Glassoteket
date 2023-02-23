using Microsoft.EntityFrameworkCore;
using mikaeleriksson.Shared;

namespace mikaeleriksson.Server.Data;

public class GlassotekDbContext : DbContext
{
    public DbSet<Favorite> FavoriteIceCreams { get; set; }

    public DbSet<UserModel> Users { get; set; }
    public GlassotekDbContext(DbContextOptions options) : base(options)
    {

    }
}
