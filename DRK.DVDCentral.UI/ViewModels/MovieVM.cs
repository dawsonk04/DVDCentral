using DRK.DVDCentral.BL;
using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.UI.ViewModels
{
    public class MovieVM
    {
        public BL.Models.Movie Movie { get; set; } = new BL.Models.Movie();

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public List<Director> Directors { get; set; } = new List<Director>();

        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public List<Format> Formats { get; set; } = new List<Format>();

        public IFormFile File { get; set; }

        public IEnumerable<int> GenreIds { get; set; }

        public MovieVM()
        {
            Genres = GenreManager.Load();

            Directors = DirectorManager.Load();

            Ratings = RatingManager.Load();

            Formats = FormatManager.Load();
        }

        public MovieVM(int id)
        {
            Movie = MovieManager.LoadById(id);
            Genres = GenreManager.Load();
            Directors = DirectorManager.Load();
            Ratings = RatingManager.Load();
            Formats = FormatManager.Load();
            GenreIds = Movie.Genres.Select(a => a.Id);
        }




    }
}
