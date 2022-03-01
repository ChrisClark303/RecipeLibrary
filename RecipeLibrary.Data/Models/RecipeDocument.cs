using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeLibrary.Core
{
    public class RecipeDocument : DocumentBase
    {
        public string Name { get; set; }
        public int Serves { get; set; }
        public RecipeIngredientDocument[] Ingredients { get; set; }
        public double Calories { get; set; }
    }

    public class DocumentBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
