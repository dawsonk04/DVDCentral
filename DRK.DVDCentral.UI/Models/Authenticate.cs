using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.UI.Extensions;

namespace DRK.DVDCentral.UI.Models
{
    public static class Authenticate
    {
        public static bool isAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                return true;
            }
            else { return false; }
        }
    }
}
