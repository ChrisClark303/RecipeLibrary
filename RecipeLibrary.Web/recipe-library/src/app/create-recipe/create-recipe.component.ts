import { Component, OnInit } from '@angular/core';
import { IngredientListComponent } from '../ingredient-list/ingredient-list.component';
import { RecipeIngredient } from '../RecipeIngredient';
import {RecipesService} from '../recipes.service';
import {Recipe} from '../Recipe';
import { IngredientQuantityListComponent } from '../ingredient-quantity-list/ingredient-quantity-list.component';
import { RecipeIngredientsPipe } from '../ingredients.pipe';
import { IngredientListResolverService } from '../ingredient-list-resolver.service';
import { IngredientListResolverInterface } from '../IngredientListResolverInterface';

@Component({
  selector: 'app-create-recipe',
  templateUrl: './create-recipe.component.html',
  styleUrls: ['./create-recipe.component.css']
})
export class CreateRecipeComponent implements OnInit {

  recipe: Recipe;
  ingredientResolver: IngredientListResolverInterface;
  //selectedIngredients: RecipeIngredient[];

  constructor(private recipeService: RecipesService,
              ingredientResolver: IngredientListResolverService) {
    this.ingredientResolver = ingredientResolver;
    this.initializeRecipe();
   }

  private initializeRecipe() {
    this.recipe = { recipeId: "", ingredients: new Array<RecipeIngredient>(), name: "", serves: 0 };
  }

  ngOnInit(): void {
  }

  createRecipe() {
    this.recipeService.addRecipe(this.recipe)
      .subscribe(() => this.initializeRecipe());
  }
}
