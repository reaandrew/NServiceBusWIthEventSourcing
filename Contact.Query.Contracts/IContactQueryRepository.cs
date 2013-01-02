using System;
using System.Collections.Generic;

namespace Contact.Query.Contracts
{
    public interface IContactQueryRepository
    {
        void Save(Model.AccommodationLead accommodationLead);
        void Save(Model.AccommodationSupplier accommodationSupplier);
        void Save(Model.Authentication authentication);
        void Save(Model.User user);

        List<Model.AccommodationLead> ListAccommodationLeads();
        Model.AccommodationLead GetAccommodationLeadById(Guid id);
    }
}
