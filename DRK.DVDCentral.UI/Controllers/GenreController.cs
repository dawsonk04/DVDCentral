using DRK.DVDCentral.BL;
using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DRK.DVDCentral.UI.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            return View(GenreManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(GenreManager.LoadById(id));
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

        public IActionResult Create(Genre genre)
        {
            try
            {
                int result = GenreManager.Insert(genre);
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
                return View(GenreManager.LoadById(id));

            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }


        }
        [HttpPost]

        public IActionResult Edit(int id, Genre genre, bool rollback = false)
        {
            try
            {
                int result = GenreManager.Update(genre, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(genre);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(GenreManager.LoadById(id));

        }
        [HttpPost]

        public IActionResult Delete(int id, Genre genre, bool rollback = false)
        {
            try
            {
                int result = GenreManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(genre);
            }
        }



    }
}
