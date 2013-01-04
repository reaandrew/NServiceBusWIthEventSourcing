using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using Contact.Query.Contracts.Model;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace Contact.WebApi.AcceptanceTests
{
    [Binding]
    public class StepDefinitions
    {
        private const string AccLeadIdKey = "AccLeadId";
        private const string AccLeadKey = "AccLead";

        private string ApiHost
        {
            get { return ConfigurationManager.AppSettings["ApiHost"]; }
        }

        [Given(@"an Accommodation Lead exists with the following information:")]
        public void GivenAnAccommodationLeadExistsWithTheFollowingInformation(Table table)
        {
            var tableRow = table.Rows[0];
            var accLeadId = CreateAccommodationLead(tableRow["Name"], tableRow["Email"]);
            ScenarioContext.Current.Set(accLeadId, AccLeadIdKey);
            Thread.Sleep(1000);
        }

        [When(@"I approve the Accommodation Lead")]
        public void WhenIApproveTheAccommodationLead()
        {
            var accLeadId = ScenarioContext.Current.Get<Guid>(AccLeadIdKey);
            ApproveAccommodationLead(accLeadId);
            Thread.Sleep(1000);
        }

        [When(@"I query the system for the Accommodation Lead")]
        public void WhenIQueryTheSystemForTheAccommodationLead()
        {
            var accLeadId = ScenarioContext.Current.Get<Guid>(AccLeadIdKey);
            var accLead = GetAccommodationLead(accLeadId);
            ScenarioContext.Current.Set(accLead, AccLeadKey);
        }

        [Then(@"the Approval status of the Accommodation Lead should be true")]
        public void ThenTheApprovalStatusOfTheAccommodationLeadShouldBeTrue()
        {
            var accLead = ScenarioContext.Current.Get<AccommodationLead>(AccLeadKey);
            Assert.That(accLead.Approved, Is.True);
        }

        private void ApproveAccommodationLead(Guid id)
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest {Method = Method.PUT, Resource = "api/accommodationleads/approved"};
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("Id", id, ParameterType.GetOrPost);
            client.Execute(request);
        }

        private AccommodationLead GetAccommodationLead(Guid id)
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest {Method = Method.GET, Resource = "api/accommodationleads/" + id.ToString("N")};
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            return client.Execute<AccommodationLead>(request).Data;
        }

        private Guid CreateAccommodationLead(string name, string email)
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest {Method = Method.POST, Resource = "api/accommodationleads"};
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("Name", name, ParameterType.GetOrPost);
            request.AddParameter("Email", email, ParameterType.GetOrPost);

            var response = client.Execute(request);
            var location = response.Location();
            var id = Guid.Parse(location.Substring(location.LastIndexOf("/")).TrimStart('/'));

            return id;
        }
    }

    public static class RestSharpExtensions
    {
        public static string Location(this IRestResponse response)
        {
            var header = response.Headers.SingleOrDefault(x => x.Name == "Location");
            return header == null ? null : (string) header.Value;
        }
    }
}