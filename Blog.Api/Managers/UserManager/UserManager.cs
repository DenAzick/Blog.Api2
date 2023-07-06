using Blog.Api.Context;
using Blog.Api.Entities;
using Blog.Api.Models;
using Blog.Api.Models.IdentityModels;
using Blog.Api.Managers.UserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Managers.UserManager;

public class UserManager
{
    private readonly AppDbContext _context;
    private ILogger<UserManager> _logger;
    private readonly JwtTokenManager _jwtTokenManager;

    public UserManager(AppDbContext context, ILogger<UserManager> logger, JwtTokenManager jwtTokenManager)
    {
        _context = context;
        _logger = logger;
        _jwtTokenManager = jwtTokenManager;
    }


    public async Task<User> Register(CreateUserModel model)
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                throw new Exception("Username already exists");
            }

            var user = new User()
            {
                Name = model.Name,
                Username = model.Username
            };

            user.Password = new PasswordHasher<User>().HashPassword(user, model.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while registering the user.", ex);
        }


    }

    public async Task<string> Login(LoginUserModel model)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null)
            {
                throw new Exception("Username or Password is incorrect");
            }

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Username or Password is incorrect");
            }

            var token = _jwtTokenManager.GenerateToken(user);

            return token;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while logging in.", ex);
        }
    }

    public async Task<User?> GetUser(string username)
    {
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (result == null)
        {
            throw new Exception("User not found");
        }

        return result;
    }
    public async Task<User?> GetUser(Guid id)
    {
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (result == null)
        {
            throw new Exception("User not found");
        }

        return result;
    }

}