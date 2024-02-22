using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre
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
            Assert.AreEqual(3, dc.tblGenres.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblGenre entity = new tblGenre();

            entity.Description = "Test";
            entity.Id = -99;
            // add entity to DB
            dc.tblGenres.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDeclaration - use the first one
            tblGenre entity = dc.tblGenres.FirstOrDefault();

            // Change property values
            entity.Description = "test";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDeclaration where id = 4
            tblGenre entity = dc.tblGenres.Where(e => e.Id == 1).FirstOrDefault();

            dc.tblGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]

        public void LoadByID()
        {
            tblGenre entity = dc.tblGenres.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }

    }
}


