import { Ingredient } from './Ingredient';
import { Observable } from 'rxjs';

export interface IngredientListResolverInterface {
    searchIngredients(term: string): Observable<Ingredient[]>;
    getIngredients(): Observable<Ingredient[]>;
}