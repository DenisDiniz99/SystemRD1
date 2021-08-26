using Microsoft.AspNetCore.Mvc;

namespace SystemRD1.WebApp.Controllers
{
    public class CustomersController : WebController
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}
