using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    Task<User> ValidateUser(string email, string password);
    Task RegisterUser(User user);
    Task UpdateUserAsync(UserUpdateDto user);
}