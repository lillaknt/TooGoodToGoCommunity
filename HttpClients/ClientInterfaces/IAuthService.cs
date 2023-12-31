using System.Security.Claims;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IAuthService
{
    Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(string email, string password);
    public Task LogoutAsync();
    public Task RegisterAsync(User user);
    public Task<ClaimsPrincipal> GetAuthAsync();
}