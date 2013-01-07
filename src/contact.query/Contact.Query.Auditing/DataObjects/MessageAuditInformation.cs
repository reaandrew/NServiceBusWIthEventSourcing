using System;
using MongoDB.Bson;

namespace Contact.Query.Auditing.DataObjects
{
    public class MessageAuditInformation
    {
        public ObjectId Id { get; set; }
        public MessageAuditInformation()
        {
            TotalMilliseconds = 0;
            MessageCount = 0;
            StartDate = DateTime.Now;
        }

        public DateTime StartDate { get; set; }
        public string OriginatingQueue { get; set; }
        public string MessageTypeName { get; set; }
        public double TotalMilliseconds { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public int MessageCount { get; set; }
    }
}