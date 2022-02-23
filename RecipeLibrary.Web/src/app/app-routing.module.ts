import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent }   from './dashboard/dashboard.component';
import { RecipeListComponent } from './recipe-list/recipe-list.component';
import { CreateRecipeComponent} from './create-recipe/create-recipe.component';
import { IngredientsViewComponent} from './ingredients-view/ingredients-view.component';
import { RecipeViewComponent } from './recipe-view/recipe-view.component';


const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'ingredients', component: IngredientsViewComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'recipes', component:RecipeViewComponent},
  { path: 'create-recipe', component:CreateRecipeComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
