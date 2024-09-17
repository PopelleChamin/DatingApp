using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class AccountController(DataContext contex): BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> RegisterAsync(string username, string password){
        using var hmac= new HMACSHA512();

        var user = new AppUser{
            UserNane = username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };
        contex.Users.Add(user);
        await contex.SaveChangesAsync();

        return user;
    }
}
