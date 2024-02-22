using DRK.DVDCentral.BL.Models;

namespace DRK.DVDCentral.BL.Test
{
    [TestClass]
    public class utFormat
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, FormatManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = FormatManager.Insert("Test", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            Format format = new Format
            {
                Description = "Test"
            };
            int results = FormatManager.Insert(format, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            Format format = FormatManager.LoadById(3);
            format.Description = "Test";
            int results = FormatManager.Update(format, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = FormatManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

    }
}

