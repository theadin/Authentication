using Microsoft.AspNetCore.Authorization;

namespace Authentication.Components.Pages;
[Authorize(Roles = Constants.RoleUser)]

public partial class Home
{
    private async Task logout()
    {
        _NavigationManager.NavigateTo("/Logout", true);
    }

}