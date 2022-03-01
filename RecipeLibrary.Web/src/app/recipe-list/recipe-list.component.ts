import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {RecipesService} from '../recipes.service';
import { Observable, Subject } from 'rxjs';
import {   debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import {Recipe} from '../Recipe';
//import { EventEmitter } from 'events';

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {

  @Output() onRecipeSelected = new EventEmitter<Recipe>();
  @Input() selectedRecipe: Recipe;
  recipes: Recipe[];
  recipes$: Observable<Recipe[]>;
  private searchTerms = new Subject<string>();

  constructor(private recipeService:RecipesService) { }

  search(term: string): void {
    if (!this.recipes$){
      this.recipes$ = this.searchTerms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),

      // ignore new term if same as previous term
      distinctUntilChanged(),

      // switch to new search observable each time the term changes
      switchMap((term: string) => this.recipeService.searchRecipes(term)),
    );
    }
    this.searchTerms.next(term);
    //this.recipes$.subscribe(i => this.recipes = i)
  }

  ngOnInit(): void {
    this.recipes$ = this.recipeService.getRecipes();
  }

  selectRecipe(recipe:Recipe) {
    console.log(recipe.name);
    //would need to get recipe here
    this.recipeService.getRecipeById(recipe.recipeId)
      .subscribe(x => {
        this.selectedRecipe = x;
        this.onRecipeSelected.emit(recipe)});
  }
}
