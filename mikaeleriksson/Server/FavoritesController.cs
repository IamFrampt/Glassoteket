using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using mikaeleriksson.Server.Data;
using mikaeleriksson.Shared;

namespace mikaeleriksson.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteDbContext db;

        public FavoritesController(FavoriteDbContext db)
        {
            this.db = db;
        }

        [HttpPut("{id}")]
        public async Task<Favorite> Put(Guid id, [FromBody] Favorite favoriteIceCream)
        {
            var edit = await db.FavoriteIceCreams.FindAsync(id);
            if (edit != null)
            {
                edit.Name = favoriteIceCream.Name;
                edit.Info = favoriteIceCream.Info;
                edit.Img = favoriteIceCream.Img;
                edit.Type = favoriteIceCream.Type;
                edit.Company = favoriteIceCream.Company;
                edit.Calorie = favoriteIceCream.Calorie;
                edit.Kilojoules = favoriteIceCream.Kilojoules;
                edit.Fat = favoriteIceCream.Fat;
                edit.Salt = favoriteIceCream.Salt;
                edit.Carbohydrates = favoriteIceCream.Carbohydrates;
                edit.Protein = favoriteIceCream.Protein;

                await db.SaveChangesAsync();
            }
            return edit;
        }

        [HttpDelete("{id}")]
        public async Task<Favorite> Delete(Guid id)
        {
            var delete = await db.FavoriteIceCreams.FindAsync(id);
            if (delete != null)
            {
                db.FavoriteIceCreams.Remove(delete);
                await db.SaveChangesAsync();
            }
            return null;
        }

        [HttpPost]
        public async Task<Favorite> Post([FromBody] Favorite create)
        {
            create.Id = Guid.NewGuid();
			EntityEntry<Favorite> cust = await db.FavoriteIceCreams.AddAsync(create);
            await db.SaveChangesAsync();
            return cust.Entity;
        }

        [HttpGet]
        public async Task<IEnumerable<Favorite>> Get()
        {
            return await Task.Factory.StartNew<IEnumerable<Favorite>>(() =>
           {
               return db.FavoriteIceCreams;
           });
        }
    }
}
