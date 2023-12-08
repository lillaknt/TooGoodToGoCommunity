using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        var userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }


    public Task<User?> GetByEmailAsync(string email)
    {
        var existing = context.Users.FirstOrDefault(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        var users = context.Users.AsEnumerable();
        if (searchParameters.EmailContains != null)
            users = context.Users.Where(u =>
                u.Email.Contains(searchParameters.EmailContains, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        var existing = context.Users.FirstOrDefault(u =>
            u.Id == id
        );
        return Task.FromResult(existing);
    }

    public Task UpdateUserAsync(UserUpdateDto dto)
    {
        // Check if the user with the given ID exists in the context
        User? existingUser = context.Users.FirstOrDefault(u => u.Email == dto.Email);

        if (existingUser != null)
        {
            // Update the properties of the existing user
            existingUser.FirstName = dto.FirstName;
            existingUser.PostCode = dto.PostCode;
            existingUser.ItemsPurchased = dto.ItemsPurchased;
            existingUser.CO2Saved = dto.CO2Saved; // Update other properties as needed

            // Save changes to the context
            context.SaveChanges();
        }

        return Task.CompletedTask;
    }
}