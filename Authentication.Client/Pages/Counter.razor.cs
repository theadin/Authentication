using Authentication.Client.Shared;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.Client.Pages;
[Authorize(Roles = Constants.RoleUser)]

public partial class Counter
{
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
