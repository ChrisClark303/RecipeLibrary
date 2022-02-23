using Newtonsoft.Json;
using RecipeLibrary.Models;
using System;
using System.Linq;

namespace RecipeLibrary.Core
{
        public class RecipeIngredientJsonConverter : JsonConverter<StandardIngredient>
    {
        private readonly StandardIngredient[] _standardIngredients;

        public RecipeIngredientJsonConverter(StandardIngredient[] standardIngredients)
        {
            _standardIngredients = standardIngredients;
        }

        public override void WriteJson(JsonWriter writer, StandardIngredient value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Name);
        }

        public override StandardIngredient ReadJson(JsonReader reader, Type objectType, StandardIngredient existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return _standardIngredients.FirstOrDefault(i => i.Name == s);
        }
    }
}
