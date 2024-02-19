using Microsoft.AspNetCore.Mvc;

namespace HiChat.Controllers
{
    [Controller]
    public class FallBackController : Controller
    {
        public ActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
