using RecipeLibrary.Core;

namespace RecipeLibrary.Core
{
    public class MongoSettings : IMongoSettings
    {
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
    }
}
