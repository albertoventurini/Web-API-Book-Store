using System.Security.Claims;

namespace WebAPIBookStore2.Services
{
    public class ClaimsIdentityFactory
    {
        public ClaimsIdentity Create(string username)
        {
            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim(ClaimTypes.Email, "email@email.it")
                };

            var identity = new ClaimsIdentity(claims, "Basic");
            return identity;
        } 
    }
}