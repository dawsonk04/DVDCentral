using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre
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
            Assert.AreEqual(3, dc.tblMovieGenres.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblMovieGenre entity = new tblMovieGenre();

            entity.MovieId = 1;
            entity.GenreId = 1;

            entity.Id = -99;
            // add entity to DB
            dc.tblMovieGenres.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDeclaration - use the first one
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();

            // Change property values
            entity.MovieId = 1;
            entity.GenreId = 4;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDeclaration where id = 4
            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 1).FirstOrDefault();

            dc.tblMovieGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }





    }
}
