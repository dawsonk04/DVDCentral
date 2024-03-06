namespace DRK.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovieGenre
    {

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = MovieGenreManager.Insert(1, 1, ref id, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieGenreManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }


    }
}
