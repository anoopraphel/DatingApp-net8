using API;
using API.Entities;
public interface ITokenService
{
    string CreateToken(AppUser user);
}