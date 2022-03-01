using AutoMapper;
using Newtonsoft.Json;
using RecipeLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IFileSystem _fileSystem;
        private readonly IMapper _mapper;

        public IngredientRepository(IFileSystem fileSystem, IMapper mapper)
        {
            _fileSystem = fileSystem;
            _mapper = mapper;
        }

        public async Task<StandardIngredient[]> Load()
        {
            var content = await _fileSystem.LoadDocument<StandardIngredientDocument>("ingredients", "Measurement");
            var mappedContent = _mapper.Map<StandardIngredient[]>(content);
            return mappedContent
                .ToArray();
        }

        public async Task Save(StandardIngredient[] ingredients)
        {
            var json = JsonConvert.SerializeObject(ingredients);
            await _fileSystem.Save("ingredients", json);
        }
    }
}
