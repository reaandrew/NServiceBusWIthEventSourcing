using System;

namespace Contact.Domain
{
    /// <summary>
    /// Used https://github.com/gregoryyoung/m-r/blob/master/SimpleCQRS/Domain.cs
    /// as a guide with regards to dynamic dispatch
    /// </summary>
    public class AccommodationLead : AggregateRoot
    {
        private string _name;
        private string _email;
        private bool _approved;

        public AccommodationLead(Guid id, string name, string email)
            :base()
        {
            this.ApplyChange(new AccommodationLeadCreated(id, name, email));
        }

        public void Approve()
        {
            this.ApplyChange(new AccommodationLeadApproved(this.ID));
        }

        private void Apply(AccommodationLeadCreated @event)
        {
            this.ID = @event.ID;
            this._name = @event.Name;
            this._email = @event.Email;
        }

        private void Apply(AccommodationLeadApproved @event)
        {
            this._approved = true;
        }
    }
}