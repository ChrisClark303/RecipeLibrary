import { RecipeIngredient } from './RecipeIngredient';

export interface Recipe {
    recipeId: string;
    name: string;
    serves: number;
    ingredients: RecipeIngredient[];
    calories?: number;
    caloriesPerPerson?: number
}