using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> CreateAsync(UserCreationDto dto);
    Task <IEnumerable<User>> GetAsync(string? emailContains = null);
    Task UpdateUserAsync(UserUpdateDto dto);
}