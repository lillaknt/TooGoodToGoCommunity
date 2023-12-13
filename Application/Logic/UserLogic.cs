using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

/// Implements the business logic operations related to users
public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        // Check if the email is already in use

        var existing = await userDao.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new Exception("Email already used");

        ValidateData(dto);
        // Create a new user using the provided DTO

        var toCreate = new User
        {
            Email = dto.Email,
            Password = dto.Password,
            FirstName = dto.FirstName,
            PostCode = dto.PostCode,
            ItemsPurchased = dto.ItemsPurchased,
            CO2Saved = dto.CO2Saved
        };
        // Save the user using the DAO

        var created = await userDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public async Task<User> ValidateUser(string email, string password)
    {
        // Retrieve the existing user by email

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


    public async Task UpdateUserAsync(UserUpdateDto updateDto)
    {
        // Fetch the existing user by email
        var existingUser = await userDao.GetByEmailAsync(updateDto.Email);

        if (existingUser == null) throw new Exception($"User with email {updateDto.Email} not found.");
        // Create a new UserUpdateDto based on existing user
        var userUpdateDto = new UserUpdateDto
        {
            Email = existingUser.Email,
            FirstName = updateDto.FirstName ?? existingUser.FirstName,
            PostCode = updateDto.PostCode ?? existingUser.PostCode,
            ItemsPurchased = updateDto.ItemsPurchased ?? existingUser.ItemsPurchased,
            CO2Saved = updateDto.CO2Saved ?? existingUser.CO2Saved
        };

        // Update the user using the UserUpdateDto
        await userDao.UpdateUserAsync(userUpdateDto);
    }

    public static void ValidateData(UserCreationDto userToCreate)
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