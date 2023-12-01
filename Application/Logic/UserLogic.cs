using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Exceptions;
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
        User? existing = await userDao.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new UnavailableEmailException("Email already used");

        ValidateData(dto);
        User toCreate = new User
        {
            Email = dto.Email,
            Password = dto.Password,
            FirstName = dto.FirstName,
            PostCode = dto.PostCode
        };
        
        User created = await userDao.CreateAsync(toCreate);
        
        return created;
    }

    private static void ValidateData(UserCreationDto userToCreate) //NEEDS TO BE FIXED
    {
        string firstName = userToCreate.FirstName;

        if (firstName.Length < 3) //put in validation for @
            throw new InvalidNameLengthException("First name must be at least 3 characters!");
    }
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
    
    public async Task<User> ValidateUser(string email, string password)
    {
        User? existing = await userDao.GetByEmailAsync(email);
        
        if (existing == null)
        {
            throw new UserNotFoundException("User not found");
        }

        if (!existing.Password.Equals(password))
        {
            throw new InvalidCredentialsException("Password mismatch");
        }

        return await Task.FromResult(existing);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new InvalidEmailException("Email cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        
        if (string.IsNullOrEmpty(user.FirstName))
        {
            throw new InvalidNameLengthException("First name cannot be null");
        }
        
        
        userDao.CreateAsync(user);
        
        return Task.CompletedTask;
    }
    
}