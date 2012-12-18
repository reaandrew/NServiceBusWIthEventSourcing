using NUnit.Framework;

namespace Contact.UnitTests.Domain
{
    [TestFixture]
    public class TestUser
    {
        [TestFixture]
        public class TestCreatingAUser
        {
            private string _name;
            private string _email;

            [SetUp]
            public void Setup()
            {
                _name = "Joe Bloggs";
                _email = "joe.bloggs@test.com";
            }
        }
    }
}
