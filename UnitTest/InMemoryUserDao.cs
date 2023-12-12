using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace TestProject1
{
    public class InMemoryUserDao : IUserDao
    {
        private readonly List<User> _users = new();

        public Task<User> CreateAsync(User user)
        {
            int userId = 1;
            if (_users.Any())
            {
                userId = _users.Max(u => u.Id);
                userId++;
            }

            user.Id = userId;

            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
        
        public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}