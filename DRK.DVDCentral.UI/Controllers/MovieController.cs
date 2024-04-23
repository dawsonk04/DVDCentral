using DRK.DVDCentral.BL;
using DRK.DVDCentral.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DRK.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View(MovieManager.Load());
        }

        public IActionResult Browse(int id)
        {
            return View(nameof(Index), MovieManager.Load(id));
        }

        public IActionResult Delete(int id)
        {
            var item = MovieManager.LoadById(id);
            ViewBag.Title = "Delete";
            return View(item);

        }
        [HttpPost]

        public IActionResult Delete(int id, Movie movie, bool rollback = false)
        {
            try
            {
                int result = MovieManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movie);
            }
        }


    }
}
