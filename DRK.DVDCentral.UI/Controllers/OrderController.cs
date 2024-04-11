using DRK.DVDCentral.BL;
using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DRK.DVDCentral.UI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View(OrderManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(OrderManager.LoadById(id));
        }

        public IActionResult Create()
        {
            ViewBag.Title = "create program";
            if (Authenticate.isAuthenticated(HttpContext))
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }
        [HttpPost]

        public IActionResult Create(Order order)
        {
            try
            {
                int result = OrderManager.Insert(order);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Edit(int id)
        {

            if (Authenticate.isAuthenticated(HttpContext))
            {
                return View(OrderManager.LoadById(id));

            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }


        }
        [HttpPost]

        public IActionResult Edit(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Update(order, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(OrderManager.LoadById(id));

        }
        [HttpPost]

        public IActionResult Delete(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);
            }
        }
    }
}
