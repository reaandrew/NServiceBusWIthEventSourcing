using System;
using NServiceBus.Saga;

namespace Contact.Messages.State
{
    public class CreateAccSupplierSagaData : IContainSagaData
    {
        // the following properties are mandatory
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }


        public Guid UserID { get; set; }
        public Guid AuthenticationID { get; set; }
        public Guid AccommodationSupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}