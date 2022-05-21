using System.Security.Claims;
using Application.Identity.Commands;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace WebUI.Pages.Auth;

public partial class Login
{
    private LoginCommand _loginCommand = new();
    protected override async Task OnInitializedAsync()
    {
        var state = await _stateProvider.GetAuthenticationStateAsync();
        if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
        {
            _navigationManager.NavigateTo("/");
        }
    }
    private async Task SubmitAsync()
    {
        var result = await _authenticationManager.LoginAsync(_loginCommand);
        if (!result.IsSuccess)
        {
            _snackBar.Add(result.ErrorMessage, MudBlazor.Severity.Error);
        }
    }
    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    void TogglePasswordVisibility()
    {
        if (_passwordVisibility)
        {
            _passwordVisibility = false;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _passwordVisibility = true;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }
}