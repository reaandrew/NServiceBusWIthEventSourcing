using System;

namespace Contact.Query.Mongo
{
    public class QueryObjectWrapper<T>
    {
        public T Object { get; set; }
        public Guid _id { get; set; }
    }
}