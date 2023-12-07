using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    Task<User?> GetByIdAsync(int id); 
}