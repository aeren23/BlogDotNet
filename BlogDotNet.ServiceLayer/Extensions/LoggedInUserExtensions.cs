using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Extensions
{
    public static class LoggedInUserExtensions
    {
        public static Guid GetLoggedInUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static string GetLoggedInEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
