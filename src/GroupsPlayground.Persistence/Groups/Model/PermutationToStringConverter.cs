using System;
using Newtonsoft.Json;

namespace GroupsPlayground.Persistence.Groups.Model
{
    public class PermutationToStringConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var permutation = (Permutation) value;
            writer.WriteValue(permutation.Expression);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string expression = (string)reader.Value;
            return new Permutation(expression);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Permutation);
        }
    }
}