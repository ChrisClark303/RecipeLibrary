using Newtonsoft.Json;
using RecipeLibrary.Models;
using System;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IFileSystem _fileSystem;

        public IngredientRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task<StandardIngredient[]> Load()
        {
            var content = await _fileSystem.Load("ingredients");
            return JsonConvert.DeserializeObject<StandardIngredient[]>(content, new JsonSerializerSettings()
            {
                Error = new EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>((o, e) =>
                {
                    e.ErrorContext.Handled = true;
                })
            });
        }

        public async Task Save(StandardIngredient[] ingredients)
        {
            var json = JsonConvert.SerializeObject(ingredients);
            await _fileSystem.Save("ingredients", json);
        }
    }
}
