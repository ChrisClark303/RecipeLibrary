import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipeLight } from '../RecipeLight';
import { RecipesService } from '../recipes.service';

@Component({
  selector: 'app-recipe-light-list',
  templateUrl: './recipe-light-list.component.html',
  styleUrls: ['./recipe-light-list.component.css']
})
export class RecipeLightListComponent implements OnInit {

  recipes$: Observable<RecipeLight[]>;

  constructor(private recipeService:RecipesService) { }

  ngOnInit(): void {
    this.recipes$ = this.recipeService.getRecipesLight();
  }

}
