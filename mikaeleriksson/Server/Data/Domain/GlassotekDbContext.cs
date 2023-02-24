using Microsoft.EntityFrameworkCore;
using mikaeleriksson.Server.Data.Model;
using mikaeleriksson.Shared;

namespace mikaeleriksson.Server.Data.Domain;

public class GlassotekDbContext : DbContext
{
    public DbSet<Favorite> FavoriteIceCreams { get; set; }

    public DbSet<UserModel> Users { get; set; }
    public GlassotekDbContext(DbContextOptions options) : base(options)
    {

    }
}
