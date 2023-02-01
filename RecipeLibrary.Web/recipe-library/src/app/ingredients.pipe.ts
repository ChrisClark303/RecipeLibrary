import { PipeTransform, Pipe } from '@angular/core'
import { RecipeIngredient } from './RecipeIngredient';
import { Ingredient } from './Ingredient';
import { MessageService } from './message.service';

@Pipe({ name: 'recipeIngredients' })
export class RecipeIngredientsPipe implements PipeTransform {

    constructor(
        private messageService: MessageService
    ){}

  transform(value:any): any {
    let recipeIngredients = []
    let ingredients = value as Ingredient[];
    for (let ingredient of ingredients) {
        var recipeIngredient = {ingredient : ingredient, quantity : 1} as RecipeIngredient;
        recipeIngredients.push(recipeIngredient);
        this.messageService.add("Added recipe ingredient " + recipeIngredient.ingredient.name);
    }
    return value;
  }
}