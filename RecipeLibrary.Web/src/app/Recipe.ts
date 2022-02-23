import { RecipeIngredient } from './RecipeIngredient';

export interface Recipe {
    name: string;
    serves: number;
    ingredients: RecipeIngredient[];
    calories?: number;
    caloriesPerPerson?: number
}