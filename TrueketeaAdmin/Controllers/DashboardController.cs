using Microsoft.AspNetCore.Mvc;

namespace TrueketeaAdmin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
