import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { IngredientListComponent } from './ingredient-list/ingredient-list.component';
import { HttpClientModule } from '@angular/common/http';
import {ListboxModule} from 'primeng/listbox';
import { RecipeListComponent } from './recipe-list/recipe-list.component';
import { CreateRecipeComponent } from './create-recipe/create-recipe.component';
import { IngredientsViewComponent } from './ingredients-view/ingredients-view.component';
import { AddIngredientComponent } from './add-ingredient/add-ingredient.component';
import { IngredientQuantityListComponent } from './ingredient-quantity-list/ingredient-quantity-list.component';
import { RecipeIngredientsPipe } from './ingredients.pipe';
import { MessagesComponent } from './messages/messages.component';
import { RecipeViewComponent } from './recipe-view/recipe-view.component';
import { IngredientRecipeListComponent } from './ingredient-recipe-list/ingredient-recipe-list.component';
import { RecipeLightListComponent } from './recipe-light-list/recipe-light-list.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    IngredientListComponent,
    RecipeListComponent,
    CreateRecipeComponent,
    IngredientsViewComponent,
    AddIngredientComponent,
    IngredientQuantityListComponent,
    RecipeIngredientsPipe,
    MessagesComponent,
    RecipeViewComponent,
    IngredientRecipeListComponent,
    RecipeLightListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ListboxModule,  
    FormsModule    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
