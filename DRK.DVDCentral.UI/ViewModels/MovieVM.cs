using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.UI.ViewModels
{
    public class MovieVM
    {
        public BL.Models.Movie Movie { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Director> Directors { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<Format> Formats { get; set; }

        //not sure if this is needed??
        public class CustomerOrder
        {

        }

    }
}
