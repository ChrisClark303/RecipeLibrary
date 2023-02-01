import { TestBed } from '@angular/core/testing';

import { IngredientListResolverService } from './ingredient-list-resolver.service';

describe('IngredientListResolverService', () => {
  let service: IngredientListResolverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IngredientListResolverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
