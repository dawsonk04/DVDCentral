﻿namespace DRK.DVDCentral.BL.Models
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

    }
}
