﻿using System;
using System.Collections.Generic;
using Contact.Domain;

namespace Contact.Core
{
    public interface IEventPublisher
    {
        void Publish<T>(T Event) where T : DomainEvent;
    }
}