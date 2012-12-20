using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contact.WebApi.Contracts.Commands;
using Contact.WebApi.Contracts.Queries;
using NServiceBus;

namespace Contact.WebApi.Controllers
{
    public class AccommodationLeadsController : ApiController
    {
        //This will be changed to implementations of ISender
        private readonly IBus _bus;

        public AccommodationLeadsController(IBus bus)
        {
            _bus = bus;
        }

        // GET api/accommodationleads
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // GET api/accommodationleads/5
        public HttpResponseMessage Get(Guid id)
        {
            HttpResponseMessage result =
                Request.CreateResponse<AccommodationLead>(HttpStatusCode.OK,
                                                          new AccommodationLead
                                                              {
                                                                  Approved = false,
                                                                  Email = "test@test.com",
                                                                  Name = "something",
                                                                  ID = Guid.NewGuid()
                                                              });
            return result;

        }

        // POST api/accommodationleads
        public HttpResponseMessage Post([FromBody]CreateAccommodationLead createAccommodationLead)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            
            response.Headers.Add("Location", "/api/accommodationleads/" + Guid.NewGuid().ToString("N"));
            return response;
        }

        // PUT api/accommodationleads/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/accommodationleads/5
        public void Delete(int id)
        {
        }
    }
}
