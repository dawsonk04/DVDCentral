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

        public IActionResult Remove(int id, bool rollback = false)
        {
            try
            {
                int result = OrderItemManager.Delete(id);
                return RedirectToAction("Index", "Order");
            }
            catch (Exception)
            {

                throw;
                return View();
            }
        }

    }
}
