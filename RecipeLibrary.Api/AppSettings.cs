using RecipeLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLibrary.Api
{
    public class AppSettings
    {
        public MongoSettings MongoSettings { get; set; }
    }

    public class MongoSettings : IMongoSettings
    {
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
