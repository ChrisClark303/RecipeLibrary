import { Component, OnInit,Input } from '@angular/core';
import { RecipeIngredient } from '../RecipeIngredient';

@Component({
  selector: 'app-ingredient-quantity-list',
  templateUrl: './ingredient-quantity-list.component.html',
  styleUrls: ['./ingredient-quantity-list.component.css']
})
export class IngredientQuantityListComponent implements OnInit {

  @Input() recipeIngredients:RecipeIngredient[];

  constructor() { }

  ngOnInit(): void {
  }

}
