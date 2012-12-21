using System;

namespace Core.Domain
{
    public abstract class DomainEvent
    {
        public int Version { get; set; }
    }
}