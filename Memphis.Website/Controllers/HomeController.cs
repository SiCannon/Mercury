using System.Web.Mvc;

namespace Memphis.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}