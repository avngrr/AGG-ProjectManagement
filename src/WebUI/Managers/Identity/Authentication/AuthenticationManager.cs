using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.Common.Interfaces.Authentication;
using Application.Identity.Commands;
using Application.Identity.Responses;
using Application.Routes.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WebUI.Authentication;

namespace WebUI.Managers.Identity.Authentication;

public class AuthenticationManager  : IAuthenticationManager
{
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationManager(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorageService)
        {
            _client = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorageService;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterCommand request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync(AuthenticationEndpoints.Register, bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegisterResponse>(registrationContent, _options);
                return result;
            }
            return new RegisterResponse { IsSuccess = true };
        }
        public async Task<LoginResponse> LoginAsync(LoginCommand userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var authResult = await _client.PostAsync(AuthenticationEndpoints.Login, bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResponse>(authContent, _options);
            if (!authResult.IsSuccessStatusCode)
                return result;
            await _localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.UserName);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new LoginResponse { IsSuccess = true };
        }
        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }