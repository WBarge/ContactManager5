import { ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';

import { ContactDetailsComponent } from './contact-details.component';
import { MessageService } from 'primeng/api';
import { Spy, createSpyFromClass } from 'jasmine-auto-spies';
import { EmailService } from '../../services/email.service';
import { PhoneService } from '../../services/phone.service';
import { HttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IEmail } from '../../Interfaces/iemail';
import { DebugElement, SimpleChange } from '@angular/core';
import { By } from '@angular/platform-browser';
import { Person } from '../../models/person';
import { EMPTY, Observable, of } from 'rxjs';
import { Email } from '../../models/email';

describe('ContactDetailsComponent', () => {
  let component: ContactDetailsComponent;
  let fixture: ComponentFixture<ContactDetailsComponent>;
  let emailServiceStub:Partial<EmailService>;

  const testEmailList:IEmail[] = [
    {
      id: '1',
      address:'test@whatever.com',
      personId:'1',
    },
    {
      id: '2',
      address:'test@whatever.net',
      personId:'1',
    }
  ]
  const testPerson:Person = new Person('1','foo','bar',false);

  const ButtonClickEvents = {
    left: { button: 0 },
    right: { button: 2 },
  };


  function click(
    el: DebugElement | HTMLElement,
    eventObj: any = ButtonClickEvents.left,
  ): void {
    if (el instanceof HTMLElement) {
      el.click();
    } else {
      el.triggerEventHandler('click', eventObj);
    }
  }

  emailServiceStub={
    getEmailAddresses(person:Person):Observable<IEmail[]>{
      return (of(testEmailList));
    },
    addEmail(email:Email):Observable<any>{
      return EMPTY;
    },
    deleteEmail(email:Email):Observable<any>{
      return EMPTY;
    }
  };


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactDetailsComponent,BrowserAnimationsModule],
      providers:[
        {provide:MessageService, useValue: createSpyFromClass(MessageService)},
        {provide:HttpClient, useValue: createSpyFromClass(HttpClient)},
        {provide:EmailService, useValue: emailServiceStub},
        {provide:PhoneService, useValue: createSpyFromClass(PhoneService)},
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactDetailsComponent);
    component = fixture.componentInstance;
    component.data=testPerson;

});

  it('should create an instance', () => {
    expect(component).toBeTruthy();
  });

  describe('Email Section',()=>{
    beforeEach(()=>{
      component.emails = testEmailList;
      fixture.detectChanges();
    });

    it ('click add button should add a new record to the data set',fakeAsync(()=>{

      const initialListSize:number = testEmailList.length;

      let addButton:DebugElement = fixture.debugElement.query(By.css('#add-email-button'));
      click(addButton);
      expect(component.emails.length).toBe(initialListSize+1);
    }));
  });
});
