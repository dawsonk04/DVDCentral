using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DRK.DVDCentral.BL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public DateTime ShipDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        //OrderItems is never Null now
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        [DisplayName("SubTotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal { get { return OrderItems.Sum(x => x.Cost); } }


        [DisplayName("Tax")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return OrderItems.Sum(x => x.Cost) * .055; } }

        [DisplayName("Final Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }


        [DisplayName("Username")]
        public string? UserName { get; set; }

        [DisplayName("User First Name")]
        public string? UserFirstName { get; set; }

        [DisplayName("User Last Name")]
        public string? UserLastName { get; set; }

        [DisplayName("User Full Name")]
        public string? UserFullName { get { return UserLastName + ", " + UserFirstName; } }

        [DisplayName("Customer Name")]
        public string? CustomerFirstName { get; set; }

        [DisplayName("Customer Last Name")]
        public string? CustomerLastName { get; set; }

        [DisplayName("Customer Full Name")]
        public string? CustomerFullName { get { return CustomerLastName + ", " + CustomerFirstName; } }



    }
}
