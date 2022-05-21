namespace WebUI.Shared.Components;

public partial class UserCard
{
    private string _firstName = "Enzo";
    private string _lastName = "Desmidt";
    private string _email = "enzodesmidt@gmail.com";
    private string _FirstLetter = "E";
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadDataAsync();
        }
    }
    private async Task LoadDataAsync()
    {
        var state = await _stateProvider.GetAuthenticationStateAsync();
        var user = state.User;

    }
}