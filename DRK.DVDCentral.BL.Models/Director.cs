namespace DRK.DVDCentral.BL.Models
{
    public class Director
    {
        public int Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }


        public String? FullName => $"{FirstName} {LastName}";
    }
}
