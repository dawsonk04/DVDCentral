using DRK.DVDCentral.BL;
using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DRK.DVDCentral.UI.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View(CustomerManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(CustomerManager.LoadById(id));
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

        public IActionResult Create(Customer customer)
        {
            try
            {
                int result = CustomerManager.Insert(customer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Edit(int id)
        {
            ViewBag.Title = "create program";
            if (Authenticate.isAuthenticated(HttpContext))
            {
                return View(CustomerManager.LoadById(id));

            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }


        }
        [HttpPost]

        public IActionResult Edit(int id, Customer customer, bool rollback = false)
        {
            try
            {
                int result = CustomerManager.Update(customer, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(CustomerManager.LoadById(id));

        }
        [HttpPost]

        public IActionResult Delete(int id, Customer customer, bool rollback = false)
        {
            try
            {
                int result = CustomerManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);
            }
        }
    }
}
