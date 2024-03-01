import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactDetailsComponent } from './contact-details.component';
import { MessageService } from 'primeng/api';
import { createSpyFromClass } from 'jasmine-auto-spies';
import { EmailService } from '../../services/email.service';
import { PhoneService } from '../../services/phone.service';
import { HttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('ContactDetailsComponent', () => {
  let component: ContactDetailsComponent;
  let fixture: ComponentFixture<ContactDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactDetailsComponent,BrowserAnimationsModule],
      providers:[
        {provide:MessageService, useValue: createSpyFromClass(MessageService)},
        {provide:HttpClient, useValue: createSpyFromClass(HttpClient)},
        {provide:EmailService, useValue: createSpyFromClass(EmailService)},
        {provide:PhoneService, useValue: createSpyFromClass(PhoneService)},
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
