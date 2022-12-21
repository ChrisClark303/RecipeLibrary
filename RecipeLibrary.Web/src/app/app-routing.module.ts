import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent }   from './dashboard/dashboard.component';
import { RecipeListComponent } from './recipe-list/recipe-list.component';
import { CreateRecipeComponent} from './create-recipe/create-recipe.component';
import { IngredientsViewComponent} from './ingredients-view/ingredients-view.component';
import { RecipeViewComponent } from './recipe-view/recipe-view.component';
import { RecipeLightListComponent } from './recipe-light-list/recipe-light-list.component';


const routes: Routes = [
  { path: '', redirectTo: '/recipes', pathMatch: 'full' },
  //{ path: 'ingredients', component: IngredientsViewComponent },
   { path: 'ingredients', redirectTo: '/recipes'}, //component: IngredientsViewComponent },
   { path: 'dashboard', redirectTo: '/recipes'}, //component: DashboardComponent },
   { path: 'recipes', component:RecipeLightListComponent},
   { path: 'create-recipe', redirectTo: '/recipes'} //component:CreateRecipeComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
