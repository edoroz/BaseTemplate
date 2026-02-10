using Microsoft.AspNetCore.Mvc;

namespace BaseTemplate.Web.Controllers {
    public class UserController : Controller {
        public IActionResult Index() {
            ViewBag.Title = "System Users";
            return View();
        }
    }
}
