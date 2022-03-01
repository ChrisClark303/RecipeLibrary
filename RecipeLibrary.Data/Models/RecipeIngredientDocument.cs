using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeLibrary.Core
{
    public class RecipeIngredientDocument
    {
        public string Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public double Calories { get; set; }
    }

    public class StandardIngredientDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public MeasurementDocument MeasurementInfo { get; set; }
        public string Measurement { get; set; }
        public int Calories { get; set; }
    }

    public class MeasurementDocument
    {
        public decimal Quantity { get; set; }
        public string Measure { get; set; }
    }
}
