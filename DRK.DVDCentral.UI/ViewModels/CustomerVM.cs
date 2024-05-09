using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.UI.ViewModels
{
    public class CustomerVM
    {
        // Checkpoint #7
        public Customer customer { get; set; }
        public List<User> users { get; set; } = new List<User>();
    }
}
