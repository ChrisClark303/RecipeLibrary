import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateRecipeLightComponent } from './create-recipe-light.component';

describe('CreateRecipeLightComponent', () => {
  let component: CreateRecipeLightComponent;
  let fixture: ComponentFixture<CreateRecipeLightComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateRecipeLightComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateRecipeLightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
