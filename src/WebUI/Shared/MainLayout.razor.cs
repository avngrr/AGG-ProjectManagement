namespace WebUI.Shared;
public partial class MainLayout
{
    private bool _drawerOpen = true;

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private async Task Logout()
    {
        await _authenticationManager.LogoutAsync();
        _navigationManager.NavigateTo("/");
    }
}