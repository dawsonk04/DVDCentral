using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.BL
{
    public static class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Movie movie)
        {
            if (movie != null) { cart.Items.Add(movie); }
        }
        public static void Remove(ShoppingCart cart, Movie movie)
        {
            if (movie != null) { cart.Items.Remove(movie); }
        }

        public static void Checkout(ShoppingCart cart)
        {
            // ## Stuff for DVD Central (#7) - Instructions, Make sure to add try catch and what not
            // Make a new Order
            // Set the order Fields as need

            // forEach(Movie item in cart.Items)

            // Make new orderItem
            // Set the OrderItem fields for the item
            // order.OrderItems.Add(orderItem)

            // OrderManager.Insert(order)

            // Decrement the tblMovie.InStkQty appropiately
            // Prolly add a Checkpoint Test
            try
            {
                Order order = new Order()
                {
                    CustomerId = 1,
                    UserId = 1,
                    OrderDate = DateTime.Now,
                    ShipDate = DateTime.Now.AddDays(2)
                };

                OrderManager.Insert(order);

                foreach (Movie item in cart.Items)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        MovieId = item.Id,
                        OrderId = order.Id,
                        Cost = item.Cost * item.Quantity,
                        Quantity = item.Quantity
                    };

                    OrderItemManager.Insert(orderItem);

                    // add stuff here for quantity?
                    int UpdatedQuantity = item.InStkQty - item.Quantity;
                    MovieManager.UpdatedStockQuantity(item.Id, UpdatedQuantity);

                }
                cart = new ShoppingCart();

            }
            catch (Exception)
            {
                throw;
            }





        }


    }
}
