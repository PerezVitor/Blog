using Blog.Models;
using System.Security.Claims;

namespace Blog.Extensions
{
    public static class UserExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var roles = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            roles.AddRange(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
            );

            return roles;
        }
    }
}
