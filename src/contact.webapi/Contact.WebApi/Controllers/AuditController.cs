using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contact.Query.Auditing.DataAccess;
using Contact.WebApi.Models;

namespace Contact.WebApi.Controllers
{
    public class AuditController : ApiController
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public AuditController(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        // GET api/audit
        public IEnumerable<MessageProcessingAuditInformation> Get()
        {
            return _auditInformationRepository.List()
                                              .Select(x => new MessageProcessingAuditInformation
                                                  {
                                                      AverageProcessingTime = (x.TotalMilliseconds/x.MessageCount).ToString("0.00"),
                                                      MinProcessingTime = x.Min.ToString("0.00"),
                                                      MaxProcessingTime = x.Max.ToString("0.00"),
                                                      MessageType = x.MessageTypeName.Replace("Contact.Messages.",""),
                                                      OriginatingAddress = x.OriginatingQueue.Replace("contact.","")
                                                  });
        }
    }
}
