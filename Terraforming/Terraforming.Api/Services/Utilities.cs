using System;
using System.Linq;
using System.Security.Claims;

namespace Terraforming.Api.Services
{
    public static class Utilities
    {

        public static string GetUserId(this ClaimsPrincipal user)
        {
            var isValid = user.HasClaim(x => x.Type == "Id");
            if (isValid)
            {
                return user.Claims.First(x => x.Type == "Id").Value;
            }
            else
            {
                throw new Exception("User account is not valid");
            }
        }
    }
}
