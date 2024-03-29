using DRK.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;

namespace DRK.DVDCentral.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(GenreManager.Load().OrderBy(g => g.Description));
        }
    }
}
