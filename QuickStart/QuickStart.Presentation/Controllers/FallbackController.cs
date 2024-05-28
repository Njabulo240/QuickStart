using Microsoft.AspNetCore.Mvc;

namespace QuickStart.Presentation.Controllers
{
    public class FallbackController : ControllerBase
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/html");
        }

    }
}
