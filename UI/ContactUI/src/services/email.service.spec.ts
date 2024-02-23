import { TestBed } from '@angular/core/testing';

import { EmailService } from './email.service';

xdescribe('EmailServiceService', () => {
  let service: EmailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
