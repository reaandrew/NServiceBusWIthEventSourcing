using System;
using System.Configuration;

namespace Core.Configuration
{
    public class EventPersistenceFactoryConfiguration : ConfigurationSection
    {
        public const string CONFIGURATION_SECTION_KEY = "EventStorePersistenceFactory";

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }

        public static IEventPersistenceFactory CreateFactory()
        {
            var configurationSection =
                (EventPersistenceFactoryConfiguration) ConfigurationManager.GetSection(CONFIGURATION_SECTION_KEY);
            return (IEventPersistenceFactory) Activator.CreateInstance(System.Type.GetType(configurationSection.Type));
        }
    }
}