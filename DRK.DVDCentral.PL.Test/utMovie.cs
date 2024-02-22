using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
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
            Assert.AreEqual(3, dc.tblMovies.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblMovie entity = new tblMovie();

            entity.Description = "Test";
            entity.Title = "Test";
            entity.FormatId = 1;
            entity.DirectorId = 1;
            entity.RatingId = 1;
            entity.InStkQty = 1;
            entity.Cost = 1;
            entity.ImagePath = "test";
            entity.Id = -99;
            // add entity to DB
            dc.tblMovies.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDeclaration - use the first one
            tblMovie entity = dc.tblMovies.FirstOrDefault();

            // Change property values
            entity.Description = "test";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDeclaration where id = 4
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 1).FirstOrDefault();

            dc.tblMovies.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]

        public void LoadByID()
        {
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }

    }
}