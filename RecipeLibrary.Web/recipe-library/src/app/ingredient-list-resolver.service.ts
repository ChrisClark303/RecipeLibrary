import { Injectable } from '@angular/core';
import { IngredientListResolverInterface } from './IngredientListResolverInterface';
import { Ingredient } from './Ingredient';
import { RecipesService } from './recipes.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IngredientListResolverService implements IngredientListResolverInterface {

  constructor(private recipeService: RecipesService) { }

  searchIngredients(term: string): Observable<Ingredient[]> {
    return this.recipeService.searchIngredients(term);
  }
  
  getIngredients(): Observable<Ingredient[]> {
    return this.recipeService.getIngredients();
  }
}
