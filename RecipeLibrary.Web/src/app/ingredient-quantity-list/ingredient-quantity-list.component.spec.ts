import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IngredientQuantityListComponent } from './ingredient-quantity-list.component';

describe('IngredientQuantityListComponent', () => {
  let component: IngredientQuantityListComponent;
  let fixture: ComponentFixture<IngredientQuantityListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IngredientQuantityListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IngredientQuantityListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
