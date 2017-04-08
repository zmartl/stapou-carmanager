using System.Web;
using System.Web.Mvc;

namespace stapolizeiuster_carmanager.Controllers
{
    public class AccountController : Controller
    {
        HomeController _homeController = new HomeController();
        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !this.Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            // you can use this for the 'authParams.state' parameter
            // in Lock, to provide a return URL after the authentication flow.
            ViewBag.State = "ru=" + HttpUtility.UrlEncode(returnUrl);

            return _homeController.Index();
        }
    }
}