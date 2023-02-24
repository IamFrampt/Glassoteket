using Microsoft.AspNetCore.Components;
using mikaeleriksson.Shared;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace mikaeleriksson.Client.Pages.IceCreams;

public partial class FavoriteIceCreams : ComponentBase
{
    public List<Favorite>? allIcecreams { get; set; }

    [CascadingParameter]
    public SearchedWord? word { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {

        allIcecreams = await PublicClient.client.GetFromJsonAsync<List<Favorite>>("/api/Favorites");
        }
        catch
        {
            allIcecreams = null;
        }
    }


    public void changeIceCream(string name)
    {
        word.Word = name;
    }


}
