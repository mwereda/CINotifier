using System.Web.Mvc;

namespace CINotifier.Controllers
{
    public class HomeController : Controller
    {       
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Admin()
        {
            return View();
        }
    }
}