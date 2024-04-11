using DRK.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;

namespace DRK.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            return View(OrderItemManager.Load());
        }

    }
}
