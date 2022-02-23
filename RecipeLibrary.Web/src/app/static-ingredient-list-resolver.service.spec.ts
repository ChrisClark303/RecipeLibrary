import { TestBed } from '@angular/core/testing';

import { StaticIngredientListResolverService } from './static-ingredient-list-resolver.service';

describe('StaticIngredientListResolverService', () => {
  let service: StaticIngredientListResolverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StaticIngredientListResolverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
