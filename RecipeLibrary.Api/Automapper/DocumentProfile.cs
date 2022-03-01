using AutoMapper;
using RecipeLibrary.Core;
using RecipeLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeLibrary.Api.Automapper
{
    public class DocumentProfile : AutoMapper.Profile
    {
        public DocumentProfile()
        {
            CreateMap<StandardIngredientDocument, StandardIngredient>();
            CreateMap<MeasurementDocument, Measurement>();

            CreateMap<RecipeDocument, Recipe>()
                .ForMember(r => r.RecipeId, o => o.MapFrom(rd => rd.Id));
            CreateMap<RecipeIngredientDocument, RecipeIngredient>()
                .ForMember(ri => ri.Ingredient, o => o.MapFrom(new StandardIngredientValueResolver().Resolve));
        }
    }

    public class StandardIngredientValueResolver : IValueResolver<RecipeIngredientDocument, RecipeIngredient, StandardIngredient>
    {
        public StandardIngredient Resolve(RecipeIngredientDocument source, RecipeIngredient destination, StandardIngredient destMember, ResolutionContext context)
        {
            if (context.Options.Items.TryGetValue("ingredients", out var ingredientsObj))
            {
                var ingredients = ingredientsObj as IEnumerable<StandardIngredient>;
                return ingredients?.FirstOrDefault(i => i.Name == source.Ingredient);
            }

            return null;
        }
    }
}
