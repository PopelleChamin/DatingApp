namespace API.Controllers;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DataEntities;
using API.DTOs;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController(
    DataContext context,
    ITokenService tokenService,
    IMapper mapper) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> RegisterAsync(RegisteRequest request)
    {

        if (await UserExistsAsync(request.Username))
        {
            return BadRequest("Username already in use");
        }

        using var hmac = new HMACSHA512();
        var user = mapper.Map<AppUser>(request);
        user.UserNane = request.Username;
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        user.PasswordSalt = hmac.Key;

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserResponse
        {
            Username = user.UserNane,
            Token = tokenService.CreateToken(user),
            KnownAs = user.KnownAs
        };
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> LoginAsync(LoginRequest request)
    {
        var user = await context.Users
        .Include(x => x.Photos)
        .FirstOrDefaultAsync(x => x.UserNane.ToLower() == request.Username.ToLower());

        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        for (var i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid username or password");
            }
        }
        return new UserResponse
        {
            Username = user.UserNane,
            KnownAs = user.KnownAs,
            Token = tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain)?.Url
        };
    }

    private async Task<bool> UserExistsAsync(string username) =>
        await context.Users.AnyAsync(u => u.UserNane.ToLower() == username.ToLower());
}
