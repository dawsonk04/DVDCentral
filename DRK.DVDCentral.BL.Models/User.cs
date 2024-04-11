namespace DRK.DVDCentral.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? UserId { get; set; }
        public String? Password { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }

    }
}
