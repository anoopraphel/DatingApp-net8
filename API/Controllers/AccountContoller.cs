using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class AccountController(DataContext context,ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] //account/register
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if (await DoseUserExist(registerDTO.Username)) return BadRequest("User already exist");

        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = registerDTO.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        var userDTO = new UserDTO
        {
            Username = registerDTO.Username.ToLower(),
            Token = tokenService.CreateToken(user)
        };
        return userDTO;
    }
    
    [HttpPost("login")] //account/register
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {

        var user =await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());

        if (user == null) return Unauthorized("Invalid user");


        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
        for (int i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }
        var userDTO = new UserDTO
        {
            Username = loginDTO.Username.ToLower(),
            Token = tokenService.CreateToken(user)
        };
        return userDTO;
    }
    private async Task<bool> DoseUserExist(string userName)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
    }
}