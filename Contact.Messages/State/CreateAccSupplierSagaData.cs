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


        //Using email until I find out how to use message headers and then I can
        //go back to using the correlation id but transport it in the headers not the explicit object
        public string Email { get; set; }
    }
}