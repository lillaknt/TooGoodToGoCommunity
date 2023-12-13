using System.Security.Claims;
using HttpClients.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IAuthService authService;

    public CustomAuthProvider(IAuthService authService)
    {
        // Inject the authentication service and subscribe to the OnAuthStateChanged event

        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }
    // Retrieve the current authentication state asynchronously

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Get the principal from the authentication service

        var principal = await authService.GetAuthAsync();
        // Return the authentication state based on the retrieved principal

        return new AuthenticationState(principal);
    }
    // Handle changes in authentication state

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        // Notify the authentication state change and provide the updated principal

        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}