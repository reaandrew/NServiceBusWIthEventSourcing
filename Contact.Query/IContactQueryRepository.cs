using System;
using System.Collections.Generic;

namespace Contact.Query
{
    public interface IContactQueryRepository
    {
        List<Model.AccommodationLead> ListAccommodationLeads();
        Model.AccommodationLead GetAccommodationLeadById(Guid id);
    }
}
