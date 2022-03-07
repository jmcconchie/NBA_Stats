using Microsoft.AspNetCore.Mvc;

namespace NBA_Stats.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.UseSelect2 = false;
            ViewBag.UseD3 = false;
        }
    }
}
