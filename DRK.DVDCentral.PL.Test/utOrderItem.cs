using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
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
            Assert.AreEqual(3, dc.tblOrderItems.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblOrderItem entity = new tblOrderItem();

            entity.OrderId = 1;
            entity.Quantity = 1;
            entity.MovieId = 1;
            entity.Cost = 1;

            entity.Id = -99;
            // add entity to DB
            dc.tblOrderItems.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDeclaration - use the first one
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();

            // Change property values
            entity.OrderId = 1;
            entity.Quantity = 1;
            entity.MovieId = 1;
            entity.Cost = 1;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDeclaration where id = 4
            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 1).FirstOrDefault();

            dc.tblOrderItems.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }





    }
}
