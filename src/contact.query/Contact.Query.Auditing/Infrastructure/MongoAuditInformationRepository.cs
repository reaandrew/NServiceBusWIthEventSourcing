using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Contact.Query.Auditing.DataAccess;
using Contact.Query.Auditing.DataObjects;
using MongoDB.Driver;
using NServiceBus;
using log4net;

namespace Contact.Query.Auditing.Infrastructure
{
    public class MongoAuditInformationRepository : IAuditInformationRepository
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly string _collectionName;

        public MongoAuditInformationRepository(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
            _collectionName = "messageProcessingTimes";
        }

        public void SaveMessage<TMessage>(TMessage message)
        {
            if (!(message is IEvent) &&
                !(message is ICommand))
                throw new ArgumentException("It is only possible to process types implementing ICommand or IEvent", "message");

            var messageTypeName = message.GetType().FullName.ToLower();
            var originatingHeader = message.GetHeader("NServiceBus.OriginatingAddress");
            var originatingQueue = originatingHeader.Substring(0, originatingHeader.IndexOf("@")).ToLower();

            var collection = GetCollection();

            var query = MongoDB.Driver.Builders.Query.And(
                MongoDB.Driver.Builders.Query<MessageAuditInformation>.EQ(x => x.MessageTypeName, messageTypeName),
                MongoDB.Driver.Builders.Query<MessageAuditInformation>.EQ(x => x.OriginatingQueue, originatingQueue)
                );

            var data = collection.FindOneAs<MessageAuditInformation>(query) ?? new MessageAuditInformation
            {
                MessageTypeName = messageTypeName,
                OriginatingQueue = originatingQueue
            };
            var processStarted = DateTime.ParseExact(message.GetHeader("NServiceBus.ProcessingStarted"),
                                                     "yyyy-MM-dd HH:mm:ss:ffffff 'Z'",
                                                     CultureInfo.InvariantCulture);
            var processEnded = DateTime.ParseExact(message.GetHeader("NServiceBus.ProcessingEnded"),
                                                   "yyyy-MM-dd HH:mm:ss:ffffff 'Z'",
                                                   CultureInfo.InvariantCulture);

            //Check if the data is inclusive of the saga
            data.MessageCount += 1;
            var milliseconds =  (double)(processEnded - processStarted).Duration().TotalMilliseconds;
            data.TotalMilliseconds += milliseconds;
            if (data.Min == 0 || milliseconds < data.Min)
                data.Min = milliseconds;
            if (data.Max == 0 || milliseconds > data.Max)
                data.Max = milliseconds;

            LogManager.GetLogger(this.GetType()).Info(messageTypeName + " NServiceBus.ProcessingStarted " + message.GetHeader("NServiceBus.ProcessingStarted"));
            LogManager.GetLogger(this.GetType()).Info(messageTypeName + " NServiceBus.ProcessingEnded " + message.GetHeader("NServiceBus.ProcessingEnded"));

            collection.Save(data);
        }

        public IList<MessageAuditInformation> List()
        {
            var collection = GetCollection();
            return collection.FindAllAs<MessageAuditInformation>().ToList();
        }

        private MongoCollection<MessageAuditInformation> GetCollection()
        {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_databaseName);
            var collection = database.GetCollection<MessageAuditInformation>(_collectionName);
            return collection;
        }
    }
}
