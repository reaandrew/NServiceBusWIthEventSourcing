using System;
using Contact.Domain;

namespace Contact.Core
{
    public interface IDomainRepository
    {
        void Save<T>(T aggregateRoot) 
            where T : AggregateRoot;

        T Get<T>(Guid id)
             where T : AggregateRoot;
    }
}