using Microsoft.AspNetCore.Components;
using mikaeleriksson.Shared;

namespace mikaeleriksson.Client.Shared;

public partial class NavMenu:ComponentBase
{
    bool collapseNavMenu = true;

    private bool expandSubNavSettings;

    string[] glassCompany = new[] { "GB", "Ben & Jerry", "Lohilo" };


    [CascadingParameter]
    public SearchedWord? word { get; set; }


    string baseMenuClass = "navbar-collapse d-sm-inline-flex ";

    string NavMenuCssClass => baseMenuClass + (collapseNavMenu ? " collapse" : "");

    void ToggleNavMenu()
    {
        if (!expandSubNavSettings)
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }

    public void changeCompany(string name)
    {
        word.Word = name;
        _navigationmanager.NavigateTo("/");
    }


}
