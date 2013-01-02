using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
