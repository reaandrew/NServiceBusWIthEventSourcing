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

        public Guid CorrelationId { get; set; }
    }
}
