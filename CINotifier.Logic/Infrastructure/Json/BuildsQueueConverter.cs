using System;
using System.Collections.Generic;
using CINotifier.Logic.Projects;
using Newtonsoft.Json;

namespace CINotifier.Logic.Infrastructure.Json
{
    internal class BuildsQueueConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (!objectType.IsGenericType)
            {
                return false;
            }

            return objectType.GetGenericTypeDefinition() == typeof(BuildsQueue<>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {            
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var objType = objectType.GetGenericArguments()[0];
            var listType = typeof(IEnumerable<>).MakeGenericType(objType);
            var list = serializer.Deserialize(reader, listType);
            var bagType = typeof(BuildsQueue<>).MakeGenericType(objType);
            var instance = Activator.CreateInstance(bagType, list);

            return instance;
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }
    }
}
