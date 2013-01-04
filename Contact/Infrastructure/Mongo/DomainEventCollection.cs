using System;
using System.Collections.Generic;
using Core.Domain;
using MongoDB.Bson;

namespace Contact.Infrastructure.Mongo
{
    public class DomainEventCollection
    {
        public ObjectId _id { get; set; }
        public Guid AggregateId { get; set; }
        public List<DomainEvent> DomainEvents { get; set; }
    }
}