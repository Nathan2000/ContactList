using ContactList.Shared.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ContactList.Web.AuthProviders
{
    public class AppAuthStateProvider : AuthenticationStateProvider
    {
        private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());

        private readonly HttpClient _http;
        private ClaimsPrincipal _cachedUser = Anonymous;

        public AppAuthStateProvider(HttpClient http)
        {
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userInfo = await _http.GetFromJsonAsync<UserInfoDto>("api/account/me");
                if (userInfo is not null && userInfo.IsAuthenticated)
                {
                    var identity = new ClaimsIdentity(
                        userInfo.Claims.Select(c => new Claim(c.Type, c.Value)),
                        authenticationType: "server");
                    _cachedUser = new ClaimsPrincipal(identity);
                }
                else
                {
                    _cachedUser = Anonymous;
                }
            }
            catch
            {
                _cachedUser = Anonymous;
            }

            return new AuthenticationState(_cachedUser);
        }

        public void NotifyUserChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
