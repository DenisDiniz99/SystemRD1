using Microsoft.AspNetCore.Mvc;

namespace SystemRD1.WebApp.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
