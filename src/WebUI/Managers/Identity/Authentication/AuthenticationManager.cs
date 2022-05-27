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
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
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
        
        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            RefreshTokenCommand command = new RefreshTokenCommand()
            {
                Token = token,
                RefreshToken = refreshToken
            };
            var bodyContent = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");
            var refreshResult = await _client.PostAsync(AuthenticationEndpoints.RefreshToken, bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResponse>(refreshContent, _options);
            if (!refreshResult.IsSuccessStatusCode)
            {
                await LogoutAsync();
                return "";
            }
            await _localStorage.SetItemAsync("authToken", result.Token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return result.Token;
        }
        public async Task<string> TryRefreshToken()
        {
            //check if token exists
            var availableToken = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(availableToken)) return string.Empty;
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 1)
                return await RefreshToken();
            return string.Empty;
        }
    }