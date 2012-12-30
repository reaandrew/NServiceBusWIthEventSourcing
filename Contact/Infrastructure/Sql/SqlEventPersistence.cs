using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Contact.Domain.DomainEvents;
using Core;
using Core.Domain;

namespace Contact.Infrastructure.Sql
{
    public class SqlEventPersistence : IEventPersistence
    {
        public class DomainEventWrapper
        {
            public DomainEventWrapper()
            {
                
            }

            public DomainEventWrapper(DomainEvent domainEvent)
            {
                DomainEvent = domainEvent;
            }

            [XmlElement]
            public DomainEvent DomainEvent { get; set; }
        }

        private readonly string _connectionString;

        public SqlEventPersistence(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(Guid aggregateId, DomainEvent domainEvent)
        {
            var serializer = CreateXmlSerializer();
            var stringBuilder = new StringBuilder();
            var xmlWriter = XmlWriter.Create(stringBuilder);
            serializer.Serialize(xmlWriter, new DomainEventWrapper(domainEvent));
            xmlWriter.Flush();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "insert into Events (Id,AggregateId,Version,Event) values (@Id,@AggregateId,@Version,@Event)";
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@AggregateId", aggregateId);
                    command.Parameters.AddWithValue("@Version", domainEvent.Version);
                    command.Parameters.AddWithValue("@Event", stringBuilder.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }

        public IList<DomainEvent> GetEventsForAggregate<T>(Guid id) where T : AggregateRoot
        {

            var serializer = CreateXmlSerializer();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Events where AggregateID = @AggregateID order by Version asc";
                    command.Parameters.AddWithValue("@AggregateID", id);
                    using (var dataReader = command.ExecuteReader())
                    {
                        var events = new List<DomainEvent>();
                        while (dataReader.Read())
                        {
                            var @eventXmlReader = dataReader.GetXmlReader(dataReader.GetOrdinal("Event"));
                            var @eventWrapper = (DomainEventWrapper) serializer.Deserialize(@eventXmlReader);
                            events.Add(@eventWrapper.DomainEvent);
                        }
                        return events;
                    }
                }
            }
        }

        private static XmlSerializer CreateXmlSerializer()
        {
            var xRoot = new XmlRootAttribute();
            xRoot.ElementName = "DomainEventWrapper";
            xRoot.IsNullable = true;
            var serializer = new XmlSerializer(typeof (DomainEventWrapper), null,
                                               new[]
                                                   {
                                                       typeof (DomainEvent),
                                                       typeof (AccommodationLeadCreated),
                                                       typeof (AccommodationLeadApproved),
                                                       typeof (UserCreated),
                                                       typeof (AuthenticationCreated),
                                                       typeof (AccommodationSupplierCreated)
                                                   }, xRoot, null);
            return serializer;
        }
    }
}