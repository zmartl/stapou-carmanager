using System.Web.Mvc;
using System.Security.Claims;

namespace stapolizeiuster_carmanager.Controllers
{
    //[Authorize]
    public class BaseController : Controller
    {
        // GET: Base
        public string GetUserNamePrinicpals()
        {
            var nameClaim = ClaimsPrincipal.Current.FindFirst("name");

            if (nameClaim != null && !string.IsNullOrEmpty(nameClaim.Value))
                return nameClaim.Value;

            return "null";
        }
    }
}