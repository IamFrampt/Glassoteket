using Microsoft.AspNetCore.Components;
using mikaeleriksson.Shared;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace mikaeleriksson.Client.Pages;

public partial class FavoriteIceCreams : ComponentBase
{
    public List<Favorite> allIcecreams { get; set; }

    [CascadingParameter]
    public SearchedWord? word { get; set; }

    string url = "https://icelabb.azurewebsites.net";


    protected override async Task OnInitializedAsync()
    {
        allIcecreams = await Pclient.client.GetFromJsonAsync<List<Favorite>>("/api/Favorites");

    }


    public void changeIceCream(string name)
    {
        word.Word = name;
    }


}
