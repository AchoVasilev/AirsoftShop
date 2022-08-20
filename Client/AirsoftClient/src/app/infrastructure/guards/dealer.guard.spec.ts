import { TestBed } from '@angular/core/testing';

import { DealerGuard } from './dealer.guard';

describe('DealerGuard', () => {
  let guard: DealerGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(DealerGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
