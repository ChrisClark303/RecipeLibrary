import { Component, OnInit, Input } from '@angular/core';
import { Recipe } from '../Recipe';
import { IngredientListResolverInterface } from '../IngredientListResolverInterface';
import { StaticIngredientListResolverService } from '../static-ingredient-list-resolver.service';

@Component({
  selector: 'app-recipe-view',
  templateUrl: './recipe-view.component.html',
  styleUrls: ['./recipe-view.component.css']
})
export class RecipeViewComponent implements OnInit {

  private _selectedRecipe:Recipe;
  ingredientResolver: IngredientListResolverInterface;
  showIngredientList: boolean = false;

  @Input()
  set selectedRecipe(recipe: Recipe) {
    this._selectedRecipe = recipe;
    console.log("selected recipe: " + recipe.name);
    this.ingredientResolver = new StaticIngredientListResolverService(this._selectedRecipe.ingredients.map(i => i.ingredient));
    console.log("Created static resolver");
  }

  get selectedRecipe(): Recipe { return this._selectedRecipe; }

  constructor() { }

  ngOnInit(): void {
  }

  onRecipeSelected(recipe:Recipe) {
    this._selectedRecipe = recipe;
    console.log("selected recipe: " + recipe.name);
    this.ingredientResolver = new StaticIngredientListResolverService(this._selectedRecipe.ingredients.map(i => i.ingredient));
    this.showIngredientList = true;
    console.log("Created static resolver");
  }


}
