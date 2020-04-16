import { TestBed } from '@angular/core/testing';

import { AuthGourdService } from './auth-guard.service';

describe('UsersGourdService', () => {
  let service: AuthGourdService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthGourdService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
