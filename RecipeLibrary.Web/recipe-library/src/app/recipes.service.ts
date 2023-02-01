import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
//import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import {Ingredient} from './Ingredient';
import {Recipe} from './Recipe';
import { RecipeLight } from './RecipeLight';

@Injectable({
  providedIn: 'root'
})
export class RecipesService {

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  private recipeServiceUrl: string = 'http://localhost/RecipeLibrary.Api';
  private v2RecipesServiceUrl : string = 'https://localhost:7153/v2'

  constructor(private httpClient: HttpClient) { }

  get<T>(url:string): Observable<T[]> {
    return this.httpClient.get<T[]>(url);
  }

  getIngredients(): Observable<Ingredient[]>
  {
      return this.get<Ingredient>(`${this.recipeServiceUrl}/ingredient`);
  }

  addIngredient(ingredient:Ingredient) : Observable<any>
  {
    return this.add(ingredient, `${this.recipeServiceUrl}/ingredient`);
    //return this.httpClient.post(`${this.recipeServiceUrl}/ingredient`, ingredient, this.httpOptions);
  }

  add<T>(content:T, url:string) : Observable<any> {
    return this.httpClient.post(url, content, this.httpOptions);
  }

  search<T>(term: string, url:string): Observable<T[]> 
  {
    if (!term.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.get<T>(`${url}?term=${term}`);
    // .pipe(
    //   tap(x => x.length ?
    //      this.log(`found heroes matching "${term}"`) :
    //      this.log(`no heroes matching "${term}"`)),
    //   catchError(this.handleError<Hero[]>('searchHeroes', []))
    //);
  }

  searchIngredients(term: string): Observable<Ingredient[]> 
  {
    return this.search<Ingredient>(term, `${this.recipeServiceUrl}/ingredient`);
  }

  getRecipes() : Observable<Recipe[]> {
    return this.httpClient.get<Recipe[]>(`${this.recipeServiceUrl}/recipe`);
  }

  getRecipesLight() : Observable<RecipeLight[]> {
    return this.httpClient.get<RecipeLight[]>(`${this.v2RecipesServiceUrl}/recipes`);
  }

  getRecipeById(id:string) : Observable<Recipe> {
    return this.httpClient.get<Recipe>(`${this.recipeServiceUrl}/recipe/${id}`)
  } 

  searchRecipes(term: string): Observable<Recipe[]> {
    return this.search<Recipe>(term, `${this.recipeServiceUrl}/recipe`);
  }

  addRecipe(recipe:Recipe) : Observable<any>
  {
    return this.add(recipe, `${this.recipeServiceUrl}/recipe`);
    //return this.httpClient.post(`${this.recipeServiceUrl}/ingredient`, ingredient, this.httpOptions);
  }

  addRecipeLight(recipe:RecipeLight) : Observable<any>
  {
    return this.add(recipe, `${this.v2RecipesServiceUrl}/recipes`);
  }

}
