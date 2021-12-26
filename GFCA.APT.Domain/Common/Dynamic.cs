using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;

namespace GFCA.APT.Domain
{
    public class Dynamic : DynamicObject
    {
        internal Dictionary<string, object> _dictionary = new Dictionary<string, object>();
        

        public object this[string propertyName]
        {
            get { return _dictionary[propertyName]; }
            set { AddProperty(propertyName, value); }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            AddProperty(binder.Name, value);
            return true;
        }

        public void AddProperty(string name, object value)
        {
            _dictionary[name] = value;
        }
    }
    public class DynamicConverter : System.Text.Json.Serialization.JsonConverter<Dynamic>
    {
        public override Dynamic Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Dynamic value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var kvp in value._dictionary)
            {
                writer.WriteString(kvp.Key, kvp.Value.ToString());
            }
            writer.WriteEndObject();
        }
    }
}
