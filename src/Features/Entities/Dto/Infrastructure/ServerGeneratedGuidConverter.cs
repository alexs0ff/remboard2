using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Dto.Infrastructure
{
    public class ServerGeneratedGuidConverter: JsonConverter<Guid>
    {
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, Guid value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read. If there is no existing value then <c>null</c> will be used.</param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override Guid ReadJson(JsonReader reader, Type objectType, Guid existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                string guidText = reader.Value?.ToString();
                if (StringComparer.OrdinalIgnoreCase.Equals(guidText, "newguid"))
                {
                    return Guid.NewGuid();
                }

                if (!Guid.TryParse(guidText, out var model))
                {
                    throw new JsonSerializationException($"Unexpected value {guidText} when parsing Guid.");
                }

                return model;

            }
            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing Guid.");
        }
    }
}
