using mikaeleriksson.Shared;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using mikaeleriksson.Shared.DTOs.Icecream;

namespace mikaeleriksson.Client.Pages;

public partial class ListIceCream : ComponentBase
{
    public List<IceCream> allIcecreams { get; set; }

    HttpResponseMessage response;

    [CascadingParameter]
    public SearchedWord? word { get; set; }

    string url = "https://icelabb.azurewebsites.net";


    protected override async Task OnInitializedAsync()
    {
        StateHasChanged();

        if(string.IsNullOrEmpty(word.Word))
        {
            response = await publicClient.client.GetAsync(url + "/GetData");
        }
        else
        {
            response = await publicClient.client.GetAsync(url + "/getbycompanyname?companyname=" + word.Word);
        }

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error");

        }

        var data = await response.Content.ReadAsStringAsync();
        allIcecreams = JsonConvert.DeserializeObject<List<IceCream>>(data);

        word.Word = null;
    }


    public void changeIceCream(string name)
    {
        word.Word= name;
    }

}
