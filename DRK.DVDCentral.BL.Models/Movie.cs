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
        public float Cost { get; set; }
        public int InStkQty { get; set; }
        public String? ImagePath { get; set; }

    }
}
