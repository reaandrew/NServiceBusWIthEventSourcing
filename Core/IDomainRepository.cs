using System;
using Core.Domain;

namespace Core
{
    public interface IDomainRepository
    {
        void Save<T>(T aggregateRoot) 
            where T : AggregateRoot;

        T Get<T>(Guid id)
             where T : AggregateRoot;
    }
}