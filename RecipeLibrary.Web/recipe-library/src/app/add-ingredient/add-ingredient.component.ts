import { Component, OnInit } from '@angular/core';
import {Ingredient} from  '../Ingredient';
import {RecipesService} from '../recipes.service';

@Component({
  selector: 'app-add-ingredient',
  templateUrl: './add-ingredient.component.html',
  styleUrls: ['./add-ingredient.component.css']
})
export class AddIngredientComponent implements OnInit {

  ingredient: Ingredient;

  constructor(private recipesService:RecipesService) {
    this.initializeIngredient();
   }

  ngOnInit(): void {
  }

  addIngredient() {
    //var ingredient = {name, measurement, calories} as Ingredient;
    this.recipesService.addIngredient(this.ingredient)
    .subscribe( () => this.initializeIngredient());
  }

  initializeIngredient()
  {
    this.ingredient = { name:"", calories: 0, measurement: ""};
  }
}
