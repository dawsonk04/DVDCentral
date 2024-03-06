using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = OrderItemManager.Insert(1, 1, 1, (float)1.5, ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            OrderItem director = new OrderItem
            {
                OrderId = 1,
                MovieId = 1,
                Quantity = 1,
                Cost = 1,

            };
            int results = OrderItemManager.Insert(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            OrderItem director = OrderItemManager.LoadById(3);
            director.OrderId = 1;
            director.Quantity = 1;
            director.MovieId = 1;
            director.Cost = 1;


            int results = OrderItemManager.Update(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderItemManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

    }
}
