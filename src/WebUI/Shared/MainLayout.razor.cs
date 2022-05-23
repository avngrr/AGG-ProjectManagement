namespace WebUI.Shared;
public partial class MainLayout
{
    private bool _drawerOpen = true;

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
    protected async override Task OnInitializedAsync()
    {
        _Interceptor.RegisterEvent();
    }
    private async Task Logout()
    {
        await _authenticationManager.LogoutAsync();
        _navigationManager.NavigateTo("/");
    }
    public void Dispose() => _Interceptor.DisposeEvent();
}