using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(1, MovieManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = MovieManager.Insert("Test", "test", 1, 1, 1, (float)1.1, 1, "Test", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            Movie movie = new Movie
            {
                Title = "Test",
                Description = "Test",
                DirectorId = 1,
                RatingId = 1,
                FormatId = 1,
                Cost = 1,
                InStkQty = 1,
                ImagePath = "Test"

            };
            int results = MovieManager.Insert(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            Movie movie = MovieManager.LoadById(1);

            movie.Title = "Test";
            movie.Description = "Test";
            movie.DirectorId = 10;
            movie.FormatId = 10;
            movie.RatingId = 21;
            movie.Cost = (float)1.1;
            movie.InStkQty = 1;
            movie.ImagePath = "Test";



            int results = MovieManager.Update(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieManager.Delete(1, true);
            Assert.AreEqual(1, results);
        }

    }
}

