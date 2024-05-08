using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DRK.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        const double TAX = 0.055;

        public List<Movie> Items { get; set; } = new List<Movie>();

        public int NumberOfItems { get { return Items.Sum(x => x.InStkQty); } }

        [DisplayName("SubTotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal { get { return Items.Sum(x => x.Cost * x.InStkQty); } }


        [DisplayName("Tax")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * TAX; } }

        [DisplayName("Final Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + TAX; } }

    }
}
