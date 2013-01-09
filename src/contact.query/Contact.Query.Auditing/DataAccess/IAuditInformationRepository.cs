using System.Collections.Generic;
using Contact.Query.Auditing.DataObjects;

namespace Contact.Query.Auditing.DataAccess
{
    public interface IAuditInformationRepository
    {
        void SaveMessage<TMessage>(TMessage @event);
        IList<MessageAuditInformation> List();
    }
}