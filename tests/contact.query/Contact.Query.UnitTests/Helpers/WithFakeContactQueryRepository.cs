using Contact.Query.Contracts;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.Query.UnitTests.Helpers
{
    public abstract class WithFakeContactQueryRepository
    {
        protected IContactQueryRepository Repository;

        [SetUp]
        public void Setup()
        {
            Repository = MockRepository.GenerateMock<IContactQueryRepository>();
        }
    }
}