import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Ingredient } from '../Ingredient';
import { RecipeIngredient } from '../RecipeIngredient';
import { RecipesService } from '../recipes.service';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { IngredientListResolverInterface } from '../IngredientListResolverInterface';
import { IngredientListResolverService } from '../ingredient-list-resolver.service';

@Component({
  selector: 'app-ingredient-list',
  templateUrl: './ingredient-list.component.html',
  styleUrls: ['./ingredient-list.component.css']
})
export class IngredientListComponent {

  @Input() showSelectionControls:boolean = false;
  @Input() selectedIngredients: RecipeIngredient[];

  ingredients: Ingredient[];
  ingredients$: Observable<Ingredient[]>;
  private searchTerms = new Subject<string>();
  
  private _ingredientResolver: IngredientListResolverInterface;

  @Input() title:string = "Ingredients";

  @Input()
  set ingredientResolver(resolver: IngredientListResolverInterface) {
    console.log("Resolver set");
    if (resolver){
    this._ingredientResolver = resolver;
    resolver.getIngredients()
      .subscribe(i => this.ingredients = i);
    }
  }

  // Push a search term into the observable stream.
  search(term: string): void {
    if (!this.ingredients$){
      this.ingredients$ = this.searchTerms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),

      // ignore new term if same as previous term
      distinctUntilChanged(),

      // switch to new search observable each time the term changes
      switchMap((term: string) => this._ingredientResolver.searchIngredients(term)),
    );
    }
    this.searchTerms.next(term);
    this.ingredients$.subscribe(i => this.ingredients = i)
  }


  toggleItemSelection(ingredient:Ingredient, checkBoxState:Boolean): void {
     if (checkBoxState) {
       var indexOfItem = this.selectedIngredients.findIndex(ri => ri.ingredient.name == ingredient.name);
       if (indexOfItem > -1) {
         this.selectedIngredients.splice(indexOfItem, 1);
       }
       else {
        var recipeIngredient = {ingredient : ingredient, quantity : 1} as RecipeIngredient;
         this.selectedIngredients.push(recipeIngredient);
       }
     }
  }
}
