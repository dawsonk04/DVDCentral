using DRK.DVDCentral.BL;
using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.UI.Extensions;
using DRK.DVDCentral.UI.Models;
using DRK.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DRK.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public MovieController(IWebHostEnvironment host)
        {
            _host = host;
        }

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



            if (Authenticate.isAuthenticated(HttpContext))
            {
                return View(movieVM);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Create(MovieVM movieVM)
        {
            try
            {
                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;
                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                    {
                        movieVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }

                int result = MovieManager.Insert(movieVM.Movie);
                return RedirectToAction(nameof(Index));

                // add stuff dealing with GenreId?

            }
            catch (Exception)
            {
                throw;
            }
        }

        // Still need to add the edit
        public IActionResult Edit(int id)
        {
            if (Authenticate.isAuthenticated(HttpContext))
            {
                MovieVM movieVM = new MovieVM(id);

                HttpContext.Session.SetObject("genreids", movieVM.GenreIds);

                return View(movieVM);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }
        [HttpPost]
        public IActionResult Edit(int id, MovieVM movieVM, bool rollback = false)
        {
            try
            {
                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;
                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                    {
                        movieVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }

                // moviegenre things --> similar to studentadvisor in progdec?
                IEnumerable<int> newGenreIds = new List<int>();
                if (movieVM.GenreIds != null)
                    newGenreIds = movieVM.GenreIds;

                IEnumerable<int> oldGenreIds = new List<int>();
                oldGenreIds = GetObject();


                IEnumerable<int> deletes = oldGenreIds.Except(newGenreIds);
                IEnumerable<int> adds = newGenreIds.Except(oldGenreIds);

                deletes.ToList().ForEach(d => MovieGenreManager.Delete(id, d));
                adds.ToList().ForEach(a => MovieGenreManager.Insert(id, a));


                int result = MovieManager.Update(movieVM.Movie, rollback);
                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movieVM);
            }
        }

        private IEnumerable<int> GetObject()
        {
            if (HttpContext.Session.GetObject<IEnumerable<int>>("genreids") != null)
            {
                return HttpContext.Session.GetObject<IEnumerable<int>>("genreids");
            }
            else
            {
                return null;
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
