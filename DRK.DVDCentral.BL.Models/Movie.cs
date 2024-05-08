namespace DRK.DVDCentral.BL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public String? Title { get; set; }
        public String? Description { get; set; }
        public int FormatId { get; set; }
        public int DirectorId { get; set; }
        public int RatingId { get; set; }
        public double Cost { get; set; }
        public int InStkQty { get; set; } // Overall Total Quantity of things
        public String? ImagePath { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();
        public string RatingDescription { get; set; }
        public string DirectorFullName { get; set; }
        public string FormatDescription { get; set; }
        public string FullName { get; set; }

        public int Quantity { get; set; } = 1; // This is for the current amount of movies in checkout
                                               // I think I need this to update the instock quantity

    }
}
