using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, CustomerManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = CustomerManager.Insert("Test", "test", 1, "Test", "Test", "WI", "Test", "Test", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            Customer customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                UserId = 1,
                Address = "Test",
                City = "Test",
                State = "WI",
                Zip = "Test",
                Phone = "Test"


            };
            int results = CustomerManager.Insert(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            Customer customer = CustomerManager.LoadById(3);
            customer.FirstName = "Test";
            customer.LastName = "Test";
            customer.UserId = 1;
            customer.Address = "Test";
            customer.City = "Test";
            customer.State = "WI";
            customer.Zip = "Test";
            customer.Phone = "Test";



            int results = CustomerManager.Update(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = CustomerManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }



    }
}
