import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IngredientRecipeListComponent } from './ingredient-recipe-list.component';

describe('IngredientRecipeListComponent', () => {
  let component: IngredientRecipeListComponent;
  let fixture: ComponentFixture<IngredientRecipeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IngredientRecipeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IngredientRecipeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
