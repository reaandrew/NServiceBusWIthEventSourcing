using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact.WebApi.Models
{
    public class MessageProcessingAuditInformation
    {
        public string MessageType { get; set; }
        public string OriginatingAddress { get; set; }
        public string AverageProcessingTime { get; set; }
        public string MinProcessingTime { get; set; }
        public string MaxProcessingTime { get; set; }
    }
}