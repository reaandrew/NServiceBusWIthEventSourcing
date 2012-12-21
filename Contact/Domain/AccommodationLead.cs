using System;
using System.Collections.Generic;
using Contact.Domain.DomainEvents;
using Core.Domain;

namespace Contact.Domain
{
    /// <summary>
    ///     Used https://github.com/gregoryyoung/m-r/blob/master/SimpleCQRS/Domain.cs
    ///     as a guide with regards to dynamic dispatch
    /// </summary>
    public class AccommodationLead : AggregateRoot
    {
        private bool _approved;
        private string _email;
        private string _name;

        public AccommodationLead(IEnumerable<DomainEvent> domainEvents)
            : base(domainEvents)
        {
            
        }

        public AccommodationLead(Guid id, string name, string email)
        {
            ApplyChange(new AccommodationLeadCreated
                {
                    ID = id,
                    Name = name,
                    Email = email
                });
        }

        public void Approve()
        {
            if (_approved)
                throw new Exception("NO");

            ApplyChange(new AccommodationLeadApproved
                {
                    ID = ID
                });
        }

        private void Apply(AccommodationLeadCreated @event)
        {
            ID = @event.ID;
            _name = @event.Name;
            _email = @event.Email;
        }

        private void Apply(AccommodationLeadApproved @event)
        {
            _approved = true;
        }
    }
}