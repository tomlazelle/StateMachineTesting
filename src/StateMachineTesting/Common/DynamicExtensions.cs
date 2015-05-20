using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace StateMachineTesting.Common
{
    public static class DynamicExtensions
    {
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return (ExpandoObject) expando;
        }

        public static T ToPOCO<T>(ExpandoObject source, T destination) where T : class
        {
            IDictionary<string, object> dict = source;
            var type = typeof(T);

            foreach (var prop in type.GetProperties())
            {
                var lower = prop.Name.ToLower();
                var key = dict.Keys.SingleOrDefault(k => k.ToLower() == lower);

                if (key != null)
                {
                    prop.SetValue(destination, dict[key], null);
                }
            }

            return destination;
        }

        public static object ToPOCO(ExpandoObject source, object destination)
        {
            IDictionary<string, object> dict = source;
            var type = destination.GetType();

            foreach (var prop in type.GetProperties())
            {
                var lower = prop.Name.ToLower();
                var key = dict.Keys.SingleOrDefault(k => k.ToLower() == lower);

                if (key != null)
                {
                    prop.SetValue(destination, dict[key], null);
                }
            }

            return destination;
        }
    }
}