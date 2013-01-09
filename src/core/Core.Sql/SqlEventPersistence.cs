using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Core.Domain;

namespace Core.Sql
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
        private readonly XmlSerializer _xmlSerializer;

        protected SqlEventPersistence(string connectionString, XmlSerializer xmlSerializer)
        {
            _connectionString = connectionString;
            _xmlSerializer = xmlSerializer;
        }

        public static SqlEventPersistence Create(string connectionString, IEnumerable<Type> domainEventTypes)
        {
            var xmlSerializer = CreateXmlSerializer(domainEventTypes);
            return new SqlEventPersistence(connectionString, xmlSerializer);
        }

        public void Save(Guid aggregateId, DomainEvent domainEvent)
        {
            var stringBuilder = new StringBuilder();
            var xmlWriter = XmlWriter.Create(stringBuilder);
            _xmlSerializer.Serialize(xmlWriter, new DomainEventWrapper(domainEvent));
            xmlWriter.Flush();


            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText =
                            "insert into Events (Id,AggregateId,Version,Event) values (@Id,@AggregateId,@Version,@Event)";
                        command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        command.Parameters.AddWithValue("@AggregateId", aggregateId);
                        command.Parameters.AddWithValue("@Version", domainEvent.Version);
                        command.Parameters.AddWithValue("@Event", stringBuilder.ToString());
                        command.ExecuteNonQuery();
                        try
                        {
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
        }

        public IList<DomainEvent> GetEventsForAggregate<T>(Guid id) where T : AggregateRoot
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText =
                            "select * from Events where AggregateID = @AggregateID order by Version asc";
                        command.Parameters.AddWithValue("@AggregateID", id);
                        var events = new List<DomainEvent>();
                        using (var dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var @eventXmlReader = dataReader.GetXmlReader(dataReader.GetOrdinal("Event"));
                                var @eventWrapper = (DomainEventWrapper) _xmlSerializer.Deserialize(@eventXmlReader);
                                events.Add(@eventWrapper.DomainEvent);
                            }
                        }
                        try
                        {
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                        return events;
                    }
                }
            }
        }

        private static XmlSerializer CreateXmlSerializer(IEnumerable<Type> domainEventTypes)
        {
            var xRoot = new XmlRootAttribute {ElementName = "DomainEventWrapper", IsNullable = true};
            var serializer = new XmlSerializer(typeof (DomainEventWrapper), null,
                                               domainEventTypes.ToArray(), xRoot, null);
            return serializer;
        }
    }
}