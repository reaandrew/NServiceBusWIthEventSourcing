using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Query.Contracts.Model;
using RestSharp;
using TechTalk.SpecFlow;

namespace Contact.WebApi.AcceptanceTests
{
    [Binding]
    public class StepDefinitions
    {
        private string ApiHost
        {
            get { return ConfigurationManager.AppSettings["ApiHost"]; }
        }

        [Given(@"an Accommodation Lead exists with the following information:")]
        public void GivenAnAccommodationLeadExistsWithTheFollowingInformation(Table table)
        {
            var tableRow = table.Rows[0];
            ScenarioContext.Current.Pending();
        }

        [When(@"I approve the Accommodation Lead")]
        public void WhenIApproveTheAccommodationLead()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"query the system for the Accommodation Lead")]
        public void WhenQueryTheSystemForTheAccommodationLead()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the Approval status of the Accommodation Lead should be true")]
        public void ThenTheApprovalStatusOfTheAccommodationLeadShouldBeTrue()
        {
            ScenarioContext.Current.Pending();
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

        private AccommodationLead GetAccommodationLead(Guid id)
        {
            var client = new RestClient(ApiHost);
            var request = new RestRequest {Method = Method.PUT, Resource = "api/accommodationleads/" + id.ToString("N")};
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            return client.Execute<AccommodationLead>(request).Data;
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
}
