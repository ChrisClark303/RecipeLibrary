import { Component, OnInit } from '@angular/core';
import {IngredientListComponent} from '../ingredient-list/ingredient-list.component';
import { AddIngredientComponent } from '../add-ingredient/add-ingredient.component';
import { IngredientListResolverService } from '../ingredient-list-resolver.service';
import { IngredientListResolverInterface } from '../IngredientListResolverInterface';

@Component({
  selector: 'app-ingredients-view',
  templateUrl: './ingredients-view.component.html',
  styleUrls: ['./ingredients-view.component.css']
})

export class IngredientsViewComponent {

  ingredientResolver: IngredientListResolverInterface;

  constructor(ingredientResolver: IngredientListResolverService) {
    this.ingredientResolver = ingredientResolver;
   }
}
