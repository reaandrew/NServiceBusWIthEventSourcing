using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleCQRS.Support;

namespace Core.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _outstandingEvents;
        public int Version { get; private set; }
        public Guid ID { get; protected set; }

        protected AggregateRoot()
        {
            _outstandingEvents = new List<DomainEvent>();
        }

        protected AggregateRoot(IEnumerable<DomainEvent> domainEvents)
            :this()
        {
            var events = domainEvents.OrderBy(x => x.Version);
            foreach (var @event in events)
            {
                ReplayChange(@event);
            }
        }

        public List<DomainEvent> OutstandingEvents
        {
            get { return new List<DomainEvent>(_outstandingEvents); }
        }

        protected void ReplayChange<T>(T @event) where T : DomainEvent
        {
            Version = @event.Version;
            this.AsDynamic().Apply(@event);
        }

        protected void ApplyChange<T>(T @event) where T : DomainEvent
        {
            @event.Version = ++Version;
            _outstandingEvents.Add(@event);

            //This is simply a way to avoid extra code at the cost of using
            //reflection, provided by external code from CQRS.
            //A reflection less way would be to map events to handlers which I am
            //not against but I though I would give this a try as I liked the look
            //of it.
            this.AsDynamic().Apply(@event);
        }

        public void MarkChangesAsCommitted()
        {
            _outstandingEvents.Clear();
        }
    }
}

namespace SimpleCQRS.Support
{
    //FROM http://blogs.msdn.com/b/davidebb/archive/2010/01/18/use-c-4-0-dynamic-to-drastically-simplify-your-private-reflection-code.aspx
    //doesnt count to line counts :)
    // And I took this from:
    // https://raw.github.com/gregoryyoung/m-r/master/SimpleCQRS/InfrastructureCrap.DontBotherReadingItsNotImportant.cs
    internal class PrivateReflectionDynamicObject : DynamicObject
    {
        private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private static readonly IDictionary<Type, IDictionary<string, IProperty>> _propertiesOnType =
            new ConcurrentDictionary<Type, IDictionary<string, IProperty>>();

        // Simple abstraction to make field and property access consistent


        private object RealObject { get; set; }

        internal static object WrapObjectIfNeeded(object o)
        {
            // Don't wrap primitive types, which don't have many interesting internal APIs
            if (o == null || o.GetType().IsPrimitive || o is string)
                return o;

            return new PrivateReflectionDynamicObject { RealObject = o };
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            IProperty prop = GetProperty(binder.Name);

            // Get the property value
            result = prop.GetValue(RealObject, index: null);

            // Wrap the sub object if necessary. This allows nested anonymous objects to work.
            result = WrapObjectIfNeeded(result);

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            IProperty prop = GetProperty(binder.Name);

            // Set the property value
            prop.SetValue(RealObject, value, index: null);

            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            // The indexed property is always named "Item" in C#
            IProperty prop = GetIndexProperty();
            result = prop.GetValue(RealObject, indexes);

            // Wrap the sub object if necessary. This allows nested anonymous objects to work.
            result = WrapObjectIfNeeded(result);

            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            // The indexed property is always named "Item" in C#
            IProperty prop = GetIndexProperty();
            prop.SetValue(RealObject, value, indexes);
            return true;
        }

        // Called when a method is called
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = InvokeMemberOnType(RealObject.GetType(), RealObject, binder.Name, args);

            // Wrap the sub object if necessary. This allows nested anonymous objects to work.
            result = WrapObjectIfNeeded(result);

            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = Convert.ChangeType(RealObject, binder.Type);
            return true;
        }

        public override string ToString()
        {
            return RealObject.ToString();
        }

        private IProperty GetIndexProperty()
        {
            // The index property is always named "Item" in C#
            return GetProperty("Item");
        }

        private IProperty GetProperty(string propertyName)
        {
            // Get the list of properties and fields for this type
            IDictionary<string, IProperty> typeProperties = GetTypeProperties(RealObject.GetType());

            // Look for the one we want
            IProperty property;
            if (typeProperties.TryGetValue(propertyName, out property))
            {
                return property;
            }

            // The property doesn't exist

            // Get a list of supported properties and fields and show them as part of the exception message
            // For fields, skip the auto property backing fields (which name start with <)
            IOrderedEnumerable<string> propNames =
                typeProperties.Keys.Where(name => name[0] != '<').OrderBy(name => name);
            throw new ArgumentException(
                String.Format(
                    "The property {0} doesn't exist on type {1}. Supported properties are: {2}",
                    propertyName, RealObject.GetType(), String.Join(", ", propNames)));
        }

        private static IDictionary<string, IProperty> GetTypeProperties(Type type)
        {
            // First, check if we already have it cached
            IDictionary<string, IProperty> typeProperties;
            if (_propertiesOnType.TryGetValue(type, out typeProperties))
            {
                return typeProperties;
            }

            // Not cache, so we need to build it

            typeProperties = new ConcurrentDictionary<string, IProperty>();

            // First, add all the properties
            foreach (PropertyInfo prop in type.GetProperties(bindingFlags).Where(p => p.DeclaringType == type))
            {
                typeProperties[prop.Name] = new Property { PropertyInfo = prop };
            }

            // Now, add all the fields
            foreach (FieldInfo field in type.GetFields(bindingFlags).Where(p => p.DeclaringType == type))
            {
                typeProperties[field.Name] = new Field { FieldInfo = field };
            }

            // Finally, recurse on the base class to add its fields
            if (type.BaseType != null)
            {
                foreach (IProperty prop in GetTypeProperties(type.BaseType).Values)
                {
                    typeProperties[prop.Name] = prop;
                }
            }

            // Cache it for next time
            _propertiesOnType[type] = typeProperties;

            return typeProperties;
        }

        private static object InvokeMemberOnType(Type type, object target, string name, object[] args)
        {
            try
            {
                // Try to incoke the method
                return type.InvokeMember(
                    name,
                    BindingFlags.InvokeMethod | bindingFlags,
                    null,
                    target,
                    args);
            }
            catch (MissingMethodException)
            {
                // If we couldn't find the method, try on the base class
                if (type.BaseType != null)
                {
                    return InvokeMemberOnType(type.BaseType, target, name, args);
                }
                //quick greg hack to allow methods to not exist!
                return null;
            }
        }

        private class Field : IProperty
        {
            internal FieldInfo FieldInfo { get; set; }

            string IProperty.Name
            {
                get { return FieldInfo.Name; }
            }


            object IProperty.GetValue(object obj, object[] index)
            {
                return FieldInfo.GetValue(obj);
            }

            void IProperty.SetValue(object obj, object val, object[] index)
            {
                FieldInfo.SetValue(obj, val);
            }
        }

        private interface IProperty
        {
            string Name { get; }
            object GetValue(object obj, object[] index);
            void SetValue(object obj, object val, object[] index);
        }

        // IProperty implementation over a PropertyInfo
        private class Property : IProperty
        {
            internal PropertyInfo PropertyInfo { get; set; }

            string IProperty.Name
            {
                get { return PropertyInfo.Name; }
            }

            object IProperty.GetValue(object obj, object[] index)
            {
                return PropertyInfo.GetValue(obj, index);
            }

            void IProperty.SetValue(object obj, object val, object[] index)
            {
                PropertyInfo.SetValue(obj, val, index);
            }
        }
    }


    public static class PrivateReflectionDynamicObjectExtensions
    {
        public static dynamic AsDynamic(this object o)
        {
            return PrivateReflectionDynamicObject.WrapObjectIfNeeded(o);
        }
    }

    public class DelegateAdjuster
    {
        public static Action<BaseT> CastArgument<BaseT, DerivedT>(Expression<Action<DerivedT>> source)
            where DerivedT : BaseT
        {
            if (typeof(DerivedT) == typeof(BaseT))
            {
                return (Action<BaseT>)((Delegate)source.Compile());
            }
            ParameterExpression sourceParameter = Expression.Parameter(typeof(BaseT), "source");
            Expression<Action<BaseT>> result = Expression.Lambda<Action<BaseT>>(
                Expression.Invoke(
                    source,
                    Expression.Convert(sourceParameter, typeof(DerivedT))),
                sourceParameter);
            return result.Compile();
        }
    }
}