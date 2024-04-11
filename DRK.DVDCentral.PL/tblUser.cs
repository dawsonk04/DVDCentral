namespace DRK.DVDCentral.PL;

public partial class tblUser
{
    public int Id { get; set; }
    //not sure why table structure is looking for both userId and username their the same thing
    public string UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    //public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
