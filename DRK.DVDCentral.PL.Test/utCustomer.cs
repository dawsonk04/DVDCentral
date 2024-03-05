using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
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
            Assert.AreEqual(3, dc.tblCustomers.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblCustomer entity = new tblCustomer();

            entity.FirstName = "Test";
            entity.LastName = "Test";
            entity.UserId = 1;
            entity.Address = "Test";
            entity.City = "Test";
            entity.State = "WI";
            entity.ZIP = "Test";
            entity.Phone = "Test";

            entity.Id = -99;
            // add entity to DB
            dc.tblCustomers.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDeclaration - use the first one
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();

            // Change property values
            entity.FirstName = "Test";
            entity.LastName = "Test";
            entity.UserId = 1;
            entity.Address = "Test";
            entity.City = "Test";
            entity.State = "WI";
            entity.ZIP = "Test";
            entity.Phone = "Test";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDeclaration where id = 4
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 1).FirstOrDefault();

            dc.tblCustomers.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]

        public void LoadByID()
        {
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }


    }
}
