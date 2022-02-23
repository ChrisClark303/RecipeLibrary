import { Injectable } from '@angular/core';
import { Ingredient } from './Ingredient';
import { IngredientListResolverInterface } from './IngredientListResolverInterface';
import { Observable, of } from 'rxjs';

export class StaticIngredientListResolverService implements IngredientListResolverInterface {

  constructor(private ingredients:Ingredient[]) {
   }
  
   searchIngredients(term: string): Observable<Ingredient[]> {
    var matchingIngredients = this.ingredients.filter(ing => ing.name.split(" ").filter(word => word.startsWith(term)).length > 0);
    return of(matchingIngredients);
  }
  getIngredients(): Observable<Ingredient[]> {
    return of(this.ingredients);
  }
}
