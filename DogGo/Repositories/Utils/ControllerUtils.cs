using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Repositories.Utils
{
    public class ControllerUtils
    {
        public static int GetCurrentUserId(ClaimsPrincipal user)
        {
            string id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        public static string GetCurrentUserRole(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Role);
        }
    }
}
