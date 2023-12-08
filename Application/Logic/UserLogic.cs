using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        var existing = await userDao.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new Exception("Email already used");

        ValidateData(dto);
        var toCreate = new User
        {
            Email = dto.Email,
            Password = dto.Password,
            FirstName = dto.FirstName,
            PostCode = dto.PostCode
        };

        var created = await userDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public async Task<User> ValidateUser(string email, string password)
    {
        var existing = await userDao.GetByEmailAsync(email);

        if (existing == null) throw new Exception("User not found");

        if (!existing.Password.Equals(password)) throw new Exception("Password mismatch");

        return await Task.FromResult(existing);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Email)) throw new ValidationException("Email cannot be null");

        if (string.IsNullOrEmpty(user.Password)) throw new ValidationException("Password cannot be null");

        if (string.IsNullOrEmpty(user.FirstName)) throw new ValidationException("First name cannot be null");


        userDao.CreateAsync(user);

        return Task.CompletedTask;
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        var email = userToCreate.Email;
        var firstName = userToCreate.FirstName;

        if (firstName.Length < 2)
            throw new Exception("First name must be at least 2 characters!");

        if (email.Length > 100)
            throw new Exception("Email must be less than 100 characters!");

        if (!email.Contains('@')) //checks if it is an email address
            throw new Exception("Invalid email format. Please include '@' in the email address!");
    }
}