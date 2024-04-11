namespace DRK.DVDCentral.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoginSucessfulTest()
        {
            Seed();
            Assert.IsTrue(UserManager.Login(new Models.User { UserId = "bfoote", Password = "maple" }));
            Assert.IsTrue(UserManager.Login(new Models.User { UserId = "kfrog", Password = "misspiggy" }));

        }

        public void Seed()
        {
            UserManager.Seed();
        }

        [TestMethod]
        public void InsertTest()
        {

        }
        [TestMethod]
        public void LoginFailureNoUserName()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new Models.User { UserId = "", Password = "" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureBadPassword()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new Models.User { UserId = "bfoote", Password = "birch" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureBadUserName()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new Models.User { UserId = "bfote", Password = "maple" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureNoPassword()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new Models.User { UserId = "bfoote", Password = "" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


    }
}
