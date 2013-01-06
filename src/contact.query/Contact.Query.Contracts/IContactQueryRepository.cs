using System;
using System.Collections.Generic;
using Contact.Query.Contracts.Model;

namespace Contact.Query.Contracts
{
    public interface IContactQueryRepository
    {
        void Save(AccommodationLead accommodationLead);
        void Save(AccommodationSupplier accommodationSupplier);
        void Save(Authentication authentication);
        void Save(User user);

        List<AccommodationLead> ListAccommodationLeads();
        AccommodationLead GetAccommodationLeadById(Guid id);
    }
}