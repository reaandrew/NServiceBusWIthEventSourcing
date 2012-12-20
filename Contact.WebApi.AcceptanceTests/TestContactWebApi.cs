using System;
using System.Linq;
using System.Net;
using Contact.WebApi.Contracts.Queries;
using NUnit.Framework;
using RestSharp;

namespace Contact.WebApi.AcceptanceTests
{
    [TestFixture]
    public class TestContactWebApi
    {
        private const string ApiHost = "http://localhost/Contact.WebApi";

        [Test]
        public void ShouldReturnHttpStatus201WhenACreateAccommodationLeadCommandIsSend()
        {
            const string name = "Something";
            const string email = "test@test.com";
            var response = CreateAccommodationLead(name, email);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Location(), Is.StringMatching(".*/api/accommodationleads/[\\d\\w]+"));
        }

        [Test]
        public void ShouldReturnAnAccommodationLead()
        {
            const string name = "Something";
            const string email = "test@test.com";
            var createResponse = CreateAccommodationLead(name, email);
            var newLocation = createResponse.Location();

            var client = new RestClient(ApiHost);
            var request = new RestRequest { Method = Method.GET, Resource = newLocation.TrimStart('/') };
            request.AddHeader("Accept", "application/json");
            var response = client.Execute<AccommodationLead>(request);
            //Hardcoded response at this point whilst I get my head around
            //WebApi and RestSharp.  Seem good up to now.
            Assert.That(response.Data.Email, Is.EqualTo("test@test.com"));
        }

        private IRestResponse CreateAccommodationLead(string name, string email)
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest { Method = Method.POST, Resource = "api/accommodationleads" };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("Name", "SomeAccommodationLeadName", ParameterType.GetOrPost);
            request.AddParameter("Email", "test@test.com", ParameterType.GetOrPost);

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
