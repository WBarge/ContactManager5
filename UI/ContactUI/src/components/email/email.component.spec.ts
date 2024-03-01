import { ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';

import { EmailComponent } from './email.component';
import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';
import { Email } from '../../models/email';

describe('EmailComponent', () => {
  let component: EmailComponent;
  let fixture: ComponentFixture<EmailComponent>;
  let addressDE:DebugElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmailComponent);
    addressDE = fixture.debugElement.query(By.css('#address'));
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not even show',()=>{
    expect(addressDE).toBeNull();
  });

  it('should be bond to the input object',fakeAsync(()=>{
    const testEmail:Email = new Email('1','1','fbar@gmail.com');
    component.data=testEmail;
    const result = (fixture.nativeElement as HTMLElement).dispatchEvent(new Event('input'));
    fixture.detectChanges();
    fixture.whenStable().then(()=>{
      addressDE = fixture.debugElement.query(By.css('#address'));
      expect(addressDE.nativeElement.value).toBe(testEmail.address);
      });
  }));
});
