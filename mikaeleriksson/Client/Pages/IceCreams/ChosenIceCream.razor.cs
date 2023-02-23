using Microsoft.AspNetCore.Components;
using mikaeleriksson.Shared;
using mikaeleriksson.Shared.DTOs.Icecream;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace mikaeleriksson.Client.Pages.IceCreams;

public partial class ChosenIceCream : ComponentBase
{
	IceCream? iceCream { get; set; } = null;
	Favorite? newFavoriteIceCream = new Favorite();
	List<Favorite>? FIC;

	bool isFavorite = false;

	[CascadingParameter]
	public SearchedWord? word { get; set; }



	protected override async Task OnInitializedAsync()
	{
		string url = "https://icelabb.azurewebsites.net/GetByName?Name=" + word.Word;
		var response = await PublicClient.client.GetAsync(url);

		if (!response.IsSuccessStatusCode)
		{
			Console.WriteLine("Error");
		}

		var data = await response.Content.ReadAsStringAsync();
		iceCream = JsonConvert.DeserializeObject<IceCream>(data);

		newFavoriteIceCream = new Favorite()
		{
			Name = iceCream.Name,
			Info = iceCream.Info,
			Img = iceCream.Img,
			Type = iceCream.Type,
			Company = iceCream.Company,
			Calorie = iceCream.nutritionalContent.Energy.Calorie,
			Kilojoules = iceCream.nutritionalContent.Energy.Kilojoules,
			Fat = iceCream.nutritionalContent.Fat,
			Salt = iceCream.nutritionalContent.Salt,
			Carbohydrates = iceCream.nutritionalContent.Carbohydrates,
			Protein = iceCream.nutritionalContent.Protein,
		};

		isFavorite = await checkIfFavorite();

	}

	bool postLiked = false;

	private async Task<bool> checkIfFavorite()
	{
		try
		{

        var favorites = await PublicClient.client.GetFromJsonAsync<List<Favorite>>("api/Favorites");
		

		if(favorites != null)
		{
			for (int i = 0; i < favorites.Count; i++)
			{
				if (favorites[i].Name.Contains(iceCream.Name))
				{
					return true;
				}
			}
		}
		else
		{
			return false;
		}
        }
        catch
        {
            return false; ;
        }

        return false;
	}

	private void checkToAddOrRemove()
	{
		if(isFavorite)
		{
			RemoveFromFavorites();
			isFavorite= false;
		}
		else
		{
			AddToFavorites();
			isFavorite = true;
		}
	}

	private async Task AddToFavorites()
	{
		var favorites = await PublicClient.client.GetFromJsonAsync<List<Favorite>>("/api/Favorites");

		if (favorites != null)
		{
			for (int i = 0; i < favorites.Count; i++)
			{
				if (favorites[i].Name.Contains(iceCream.Name))
				{
					return;
				}
			}
			using (var msg = await PublicClient.client.PostAsJsonAsync<Favorite>("/api/Favorites", newFavoriteIceCream, CancellationToken.None))
			{
				if (msg.IsSuccessStatusCode)
				{
					FIC.Add(await msg.Content.ReadFromJsonAsync<Favorite>());
				}
			}
		}
	}

	private async Task RemoveFromFavorites()
	{
		var favorites = await PublicClient.client.GetFromJsonAsync<List<Favorite>>("/api/Favorites");

		if (favorites != null)
		{
			var index = favorites.FindIndex(x => x.Name.Contains(iceCream.Name));

			using (var msg = await PublicClient.client.DeleteAsync($"/api/Favorites/{favorites[index].Id}"))
			{
				if (msg.IsSuccessStatusCode)
				{
					FIC.RemoveAt(index);
				}
			}
		}
	}
}
