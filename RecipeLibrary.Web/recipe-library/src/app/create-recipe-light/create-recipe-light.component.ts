import { Component, OnInit } from '@angular/core';
import { RecipeLight } from '../RecipeLight';
import {RecipesService} from '../recipes.service';

@Component({
  selector: 'app-create-recipe-light',
  templateUrl: './create-recipe-light.component.html',
  styleUrls: ['./create-recipe-light.component.css']
})
export class CreateRecipeLightComponent {
  recipe: RecipeLight;

  constructor(private recipeService: RecipesService) {
    this.initializeRecipe();
  }

  createRecipe() {
    this.recipeService.addRecipeLight(this.recipe)
      .subscribe(() => this.initializeRecipe());
  }

  private initializeRecipe() {
    this.recipe = { name: "", style: "", source: "" };
  }
}
