using System;
using System.Collections.Generic;
using System.Net;
using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using Contact.WebApi.Contracts.Commands;
using Contact.WebApi.Controllers;
using Contact.WebApi.Models;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.WebApi.UnitTests
{
    [TestFixture]
    public class TestAccommodationLeadsController
    {
        private IBus _bus;
        private IContactQueryRepository _queryRepository;
        private AccommodationLeadsController _controller;

        [SetUp]
        public void Setup()
        {
            _bus = MockRepository.GenerateMock<IBus>();
            _queryRepository = MockRepository.GenerateMock<IContactQueryRepository>();
            _controller = new AccommodationLeadsController(_bus, _queryRepository);
        }

        [Test]
        public void ShouldReturnListOfAccommodationLeads()
        {
            var id = Guid.NewGuid();
            _queryRepository.Stub(x => x.ListAccommodationLeads()).Return(new List<AccommodationLead>
                {
                    new AccommodationLead {AccommodationLeadId = id}
                });
            var response = _controller.Get();
            Assert.That(response.Count, Is.EqualTo(1));
            Assert.That(response[0].AccommodationLeadId, Is.EqualTo(id));
        }

        [Test]
        public void ShouldReturnASingleAccommodationLead()
        {
            var id = Guid.NewGuid();
            var accommodationLead = new AccommodationLead {AccommodationLeadId = id};
            _queryRepository.Stub(x => x.GetAccommodationLeadById(id)).Return(accommodationLead);
            var response = _controller.Get(id);
            Assert.That(response.AccommodationLeadId, Is.EqualTo(id));
        }

        [Test]
        public void ShouldReturnAnAcceptedResponseForCreatingAnAccommodationLead()
        {
            var response = _controller.Post(new CreateAccommodationLead
                {
                    Name = "something",
                    Email = "test@test.com"
                });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
        }

        [Test]
        public void ShouldReturnALocationHeaderForTheNewlyCreatedAccommodationLead()
        {
            var response = _controller.Post(new CreateAccommodationLead
                {
                    Name = "something",
                    Email = "test@test.com"
                });
            Assert.That(response.Headers.Location.ToString(), Is.StringMatching(".*/api/accommodationleads/[\\d\\w]+"));
        }

        [Test]
        public void ShouldReturnAnAcceptedResponseForApprovingAnAccommodationLead()
        {
            var id = Guid.NewGuid();
            var response = _controller.Put(new AccommodationLeadId
                {
                    Id = id
                });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
        }
    }
}