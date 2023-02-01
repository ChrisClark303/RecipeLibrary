import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeLightListComponent } from './recipe-light-list.component';

describe('RecipeLightListComponent', () => {
  let component: RecipeLightListComponent;
  let fixture: ComponentFixture<RecipeLightListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecipeLightListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecipeLightListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
