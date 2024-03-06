using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = OrderManager.Insert(1, DateTime.Now, 1, DateTime.Now, ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            Order director = new Order
            {
                CustomerId = 1,
                OrderDate = DateTime.Now,
                UserId = 1,
                ShipDate = DateTime.Now


            };
            int results = OrderManager.Insert(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            Order director = OrderManager.LoadById(3);
            director.CustomerId = 1;
            director.OrderDate = DateTime.Now;
            director.UserId = 1;
            director.ShipDate = DateTime.Now;

            int results = OrderManager.Update(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

    }
}
