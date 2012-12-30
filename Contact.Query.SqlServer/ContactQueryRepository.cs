using System;
using System.Collections.Generic;
using System.Linq;

namespace Contact.Query.SqlServer
{
    public class ContactQueryRepository : IContactQueryRepository
    {
        public List<Model.AccommodationLead> ListAccommodationLeads()
        {
            using (var context = new TestDatabaseEntities())
            {
                return context.AccommodationLeads.Select(x =>
                                                         new Contact.Query.Model.AccommodationLead
                                                             {
                                                                 AccommodationLeadId =
                                                                     x.AccommodationLeadId,
                                                                 Email = x.Email,
                                                                 Name = x.Name,
                                                                 Id = x.Id,
                                                                 Approved = x.Approved
                                                             }).ToList();
            }
        }

        public Model.AccommodationLead GetAccommodationLeadById(Guid id)
        {
            using (var context = new TestDatabaseEntities())
            {
                return context.AccommodationLeads.Where(x => x.AccommodationLeadId == id)
                              .Select(lead => new Model.AccommodationLead
                                  {
                                      Id = lead.Id,
                                      AccommodationLeadId = lead.AccommodationLeadId,
                                      Name = lead.Name,
                                      Email = lead.Email,
                                      Approved = lead.Approved
                                  }).SingleOrDefault();
            }
        }
    }
}
