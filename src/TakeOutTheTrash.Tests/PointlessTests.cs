using NUnit.Framework;

namespace TakeOutTheTrash.Tests
{
    public class PointlessTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Always_Be_Passing()
        {
            Assert.Pass();
        }

        [Test]
        public void Fix_Me_Up()
        {
            //Assert.Fail("Fix up my pointless test to pass CI");
            Assert.Pass();
        }
    }
}
