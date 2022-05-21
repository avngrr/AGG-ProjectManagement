using Application.Identity.Commands;
using MudBlazor;

namespace WebUI.Pages.Auth;

public partial class Register
{
    private RegisterCommand _registerUserModel = new();

    private async Task SubmitAsync()
    {
        var response = await _authenticationManager.RegisterAsync(_registerUserModel);
        if (response.IsSuccess)
        {
            foreach (var message in response.Errors)
            {
                _snackBar.Add(message, Severity.Error);
            }
            _navigationManager.NavigateTo("/login");
            _registerUserModel = new RegisterCommand();
        }
        else
        {
            foreach (var message in response.Errors)
            {
                _snackBar.Add(message, Severity.Error);
            }
        }
    }

    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    private void TogglePasswordVisibility()
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