using DRK.DVDCentral.BL.Models;
using DRK.DVDCentral.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DRK.ProgDec.UI.ViewComponents
{
    public class ShoppingCartComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return View(HttpContext.Session.GetObject<ShoppingCart>("cart"));
            }
            else
            {
                return View(new ShoppingCart());
            }
        }


    }
}
