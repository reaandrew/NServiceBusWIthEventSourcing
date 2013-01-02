using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contact.Query;
using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using Contact.WebApi.Contracts.Commands;
using Contact.WebApi.Models;
using NServiceBus;

namespace Contact.WebApi.Controllers
{
    public class AccommodationLeadsController : ApiController
    {
        //This will be changed to implementations of ISender
        private readonly IBus _bus;
        private readonly IContactQueryRepository _contactQueryRepository;

        public AccommodationLeadsController(IBus bus, IContactQueryRepository contactQueryRepository)
        {
            _bus = bus;
            _contactQueryRepository = contactQueryRepository;
        }

        // GET api/accommodationleads
        public List<AccommodationLead> Get()
        {
            return _contactQueryRepository.ListAccommodationLeads();
        }

        // GET api/accommodationleads/5
        public AccommodationLead Get(Guid id)
        {
            return _contactQueryRepository.GetAccommodationLeadById(id);
        }

        // POST api/accommodationleads
        public HttpResponseMessage Post([FromBody]CreateAccommodationLead createAccommodationLead)
        {
            var accLeadId = Guid.NewGuid();
            var createAccommodationLeadCommand = new Messages.Commands.CreateAccommodationLead
                {
                    AccommodationLeadID = accLeadId,
                    Name = createAccommodationLead.Name,
                    Email = createAccommodationLead.Email
                };

            _bus.Send("Contact", createAccommodationLeadCommand);

            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            response.Headers.Add("Location", "/api/accommodationleads/" + accLeadId.ToString("N"));
            return response;
        }

        // PUT api/accommodationleads/approved/
        public HttpResponseMessage Put([FromBody]AccommodationLeadId accommodationLeadId)
        {
            var approveAccLeadCommand = new Contact.Messages.Commands.ApproveAccLead
                {
                    AccLeadId = accommodationLeadId.Id
                };
            _bus.Send("Contact", approveAccLeadCommand);
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            return response;
        }

        // DELETE api/accommodationleads/5
        public void Delete(int id)
        {
        }
    }
}
