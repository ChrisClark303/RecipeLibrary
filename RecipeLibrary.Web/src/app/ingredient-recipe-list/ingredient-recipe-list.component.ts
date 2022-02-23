import { Component, OnInit, Input } from '@angular/core';
import { RecipeIngredient } from '../RecipeIngredient';

@Component({
  selector: 'app-ingredient-recipe-list',
  templateUrl: './ingredient-recipe-list.component.html',
  styleUrls: ['./ingredient-recipe-list.component.css']
})
export class IngredientRecipeListComponent implements OnInit {

  @Input() selectedIngredients: RecipeIngredient[];
  @Input() title:string = "Ingredients";

  constructor() { }

  ngOnInit(): void {
  }

}
