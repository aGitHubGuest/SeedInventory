import { TestBed } from '@angular/core/testing';

import { SeedInventoryServiceService } from './seed-inventory-service.service';

describe('SeedInventoryServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SeedInventoryServiceService = TestBed.get(SeedInventoryServiceService);
    expect(service).toBeTruthy();
  });
});
