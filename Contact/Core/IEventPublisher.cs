using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core
{
    public interface IEventPublisher
    {
        void Publish<T>(T Event);
    }
}
