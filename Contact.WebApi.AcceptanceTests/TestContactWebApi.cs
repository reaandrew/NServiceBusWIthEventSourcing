using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using Contact.WebApi.AcceptanceTests.Drivers;
using NUnit.Framework;
using RestSharp;

namespace Contact.WebApi.AcceptanceTests
{
    [TestFixture]
    public class TestContactWebApi
    {
        private const string ApiHost = "http://localhost/Contact.WebApi";
        private ITestDataDriver _testDataDriver;

        [TestFixtureSetUp]
        public void Initialize()
        {
            var typeName = ConfigurationManager.AppSettings["TestDriverImplementation"];
            _testDataDriver = (ITestDataDriver)Activator.CreateInstance(Type.GetType(typeName));
        }

        [SetUp]
        public void Setup()
        {
            _testDataDriver.DeleteAllTestData();
        }

        [TearDown]
        public void TearDown()
        {
            _testDataDriver.DeleteAllTestData();
        }

        [Test]
        public void ShouldReturnHttpStatus202WhenACreateAccommodationLeadCommandIsSend()
        {
            const string name = "Something";
            const string email = "test@test.com";
            var response = CreateAccommodationLead(name, email);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(response.Location(), Is.StringMatching(".*/api/accommodationleads/[\\d\\w]+"));
        }

        [Test]
        public void ShouldReturnAnAccommodationLead()
        {
            const string name = "Something";
            const string email = "test@test.com";
            var createResponse = CreateAccommodationLead(name, email);
            Thread.Sleep(1000);
            var newLocation = createResponse.Location();

            var client = new RestClient(ApiHost);
            var request = new RestRequest { Method = Method.GET, Resource = newLocation.TrimStart('/') };
            request.AddHeader("Accept", "application/json");
            var response = client.Execute<Contact.Query.Contracts.Model.AccommodationLead>(request);
            Assert.That(response.Data.Email, Is.EqualTo("test@test.com"));
        }

        
        [Test]
        public void ShouldReturnAListOfAccommodationLeads()
        {
            CreateAccommodationLead("Name 1", "email1@test.com");
            CreateAccommodationLead("Name 2", "email2@test.com");
            CreateAccommodationLead("Name 3", "email3@test.com");

            Thread.Sleep(2000);

            var client = new RestClient(ApiHost);
            var request = new RestRequest {Method = Method.GET, Resource = "api/accommodationleads"};
            request.AddHeader("Accept", "application/json");
            var response = client.Execute<List<Query.Contracts.Model.AccommodationLead>>(request);
            Assert.That(response.Data.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldReturnAnAcceptHttpStatusWhenAnAccommodationLeadIsApproved()
        {
            var createResponse = CreateAccommodationLead("TestName", "test@test123.com");
            Thread.Sleep(1000);
            var id = createResponse.Location().Substring(createResponse.Location().LastIndexOf('/')).TrimStart('/');
            var approvedResponse = ApproveAccommodationLead(new Guid(id));
            Thread.Sleep(1000);

            var client = new RestClient(ApiHost);
            var request = new RestRequest {Method = Method.GET, Resource = createResponse.Location().TrimStart('/')};
            request.AddHeader("Accept", "application/json");
            var response = client.Execute<Contact.Query.Contracts.Model.AccommodationLead>(request);
            Assert.That(response.Data.Approved, Is.True);
        }

        private IRestResponse ApproveAccommodationLead(Guid id)
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest { Method = Method.PUT, Resource = "api/accommodationleads/approved" };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("Id", id, ParameterType.GetOrPost);
            return client.Execute(request);
        }

        private IRestResponse CreateAccommodationLead(string name, string email)
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest { Method = Method.POST, Resource = "api/accommodationleads" };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("Name", name, ParameterType.GetOrPost);
            request.AddParameter("Email", email, ParameterType.GetOrPost);

            return client.Execute(request);
        }
    }

    public static class RestSharpExtensions
    {
        public static string Location(this IRestResponse response)
        {
            return response.Headers.Where(x => x.Name == "Location").SingleOrDefault().Value as string;
        }
    }
}
