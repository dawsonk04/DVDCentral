using DRK.DVDCentral.BL;
using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.UI.Models;
using DRK.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
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
        public IActionResult Details(int id)
        {
            var item = MovieManager.LoadById(id);
            return View(item);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Create Movie";

            MovieVM movieVM = new MovieVM();

            movieVM.Movie = new Movie();

            movieVM.Genres = GenreManager.Load();

            movieVM.Directors = DirectorManager.Load();

            movieVM.Ratings = RatingManager.Load();

            movieVM.Formats = FormatManager.Load();

            if (Authenticate.isAuthenticated(HttpContext))
            {
                return View(movieVM);
            }
            else
            {
                return RedirectToAction("Login","User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request)});
            }
        }

        public IActionResult Create(MovieVM movieVM)
        {
            try
            {
                int result = MovieManager.Insert(movieVM.Movie);
                return RedirectToAction(nameof(Index));
            } catch (Exception)
            {
                throw;
            }
        }

        // Still need to add the edit
        public IActionResult Edit(int id)
        {
            if(Authenticate.isAuthenticated(HttpContext))
            {
                MovieVM movieVM = new MovieVM();

                movieVM.Movie = MovieManager.LoadById(id);

                movieVM.Genres = GenreManager.Load();

                movieVM.Directors = DirectorManager.Load();

                movieVM.Ratings = RatingManager.Load();

                movieVM.Formats = FormatManager.Load();

                return View(movieVM);
            } else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        public IActionResult Edit(int id, MovieVM movieVM, bool rollback = false)
        {
            try
            {
                // Added process image stuff in here??


                int result = MovieManager.Update(movieVM.Movie, rollback);
                return RedirectToAction(nameof(Index));

            } catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movieVM);
            }
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
