using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
    {
        protected DVDCentralEntities dc;
        protected IDbContextTransaction transaction;



        [TestInitialize]
        public void Initialize()
        {

            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();

        }

        [TestCleanup]

        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblOrders.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblOrder entity = new tblOrder();

            entity.CustomerId = 1;
            entity.OrderDate = DateTime.Now;
            entity.UserId = 1;
            entity.ShipDate = DateTime.Now;



            entity.Id = -99;
            // add entity to DB
            dc.tblOrders.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDeclaration - use the first one
            tblOrder entity = dc.tblOrders.FirstOrDefault();

            // Change property values
            entity.CustomerId = 1;
            entity.OrderDate = DateTime.Now;
            entity.UserId = 1;
            entity.ShipDate = DateTime.Now;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDeclaration where id = 4
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 1).FirstOrDefault();

            dc.tblOrders.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]

        public void LoadByID()
        {
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }


    }
}
