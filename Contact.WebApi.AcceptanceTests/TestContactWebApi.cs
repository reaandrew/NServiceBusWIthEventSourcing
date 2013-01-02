using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using NUnit.Framework;
using RestSharp;

namespace Contact.WebApi.AcceptanceTests
{
    [TestFixture]
    public class TestContactWebApi
    {
        private const string ApiHost = "http://localhost/Contact.WebApi";


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
            //Hardcoded response at this point whilst I get my head around
            //WebApi and RestSharp.  Seem good up to now.
            Assert.That(response.Data.Email, Is.EqualTo("test@test.com"));
        }

        /*
        [Test]
        public void ShouldReturnAListOfAccommodationLeads()
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest {Method = Method.GET, Resource = "api/accommodationsleads"};
            request.AddHeader("Accept", "application/json");
            var response = client.Execute<List<Contact.Query.Model.AccommodationLead>>(request);
            Assert.That(response.Data.Count, Is.GreaterThan(0));
        }

        [Test]
        public void ShouldValidateAuthentication()
        {
            
        }
         * */

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
