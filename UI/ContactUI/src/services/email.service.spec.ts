import { TestBed } from '@angular/core/testing';

import { EmailService } from './email.service';
import { Spy, createSpyFromClass } from 'jasmine-auto-spies';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ErrorHandlerService } from './errorHandler.service';
import { LocationService } from './location.service';
import { MessageService } from 'primeng/api';
import { Observable, of } from 'rxjs';
import { IEmail } from '../Interfaces/iemail';
import { Person } from '../models/person';
import { Email } from '../models/email';

describe('EmailService', () => {
  let service: EmailService;
  let httpSpy: Spy<HttpClient>;
  let errorHandler:Spy<ErrorHandlerService>;
  let configService:Spy<LocationService>;
  let testLocation:string = 'someLocation';
  let emailLocationResult:string = testLocation+'PersonalEmail/';

  beforeEach(() => {

    TestBed.configureTestingModule({
      providers:[
      {provide:HttpClient, useValue: createSpyFromClass(HttpClient)},
      {provide:ErrorHandlerService, useValue: createSpyFromClass(ErrorHandlerService)},
      {provide:MessageService, useValue: createSpyFromClass(MessageService)},
      {provide:LocationService, useValue: createSpyFromClass(LocationService)},
      EmailService
    ]
    });
    errorHandler = TestBed.inject<any>(ErrorHandlerService);
    errorHandler.createHandleError = MyHandleError as any;
    configService = TestBed.inject<any>(LocationService);
    configService.getLocationUrl.and.returnValue(testLocation);
    httpSpy = TestBed.inject<any>(HttpClient);

  });

  it('should be created', () => {
    service = TestBed.inject(EmailService);
    expect(service).toBeTruthy();
    expect(configService.getLocationUrl.calls.count()).toBeGreaterThanOrEqual(1);
  });

  it('should get all email', () => {

    let testData:IEmail[] = [
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

    httpSpy.get.and.nextWith(testData);

    let personData:Person = new Person('1','foo','bar',false);

    service = TestBed.inject(EmailService);
    service.getEmailAddresses(personData)
    .subscribe( (people:IEmail[])=>{
        expect(people).toHaveSize(testData.length);
      }
    );

    expect(httpSpy.get.calls.count()).toBe(1);
  });

  it('should add an email',(done: DoneFn)=>{

    let emailToinsert:Email = new Email('1','1','whatever@gmail.com');

    httpSpy.post.and.nextWith(new HttpResponse ({
      status: 200
    }));

    service = TestBed.inject(EmailService);
    service.addEmail(emailToinsert).subscribe(response => {
      expect(response.status).toEqual(200);
      done();
    });

    expect(httpSpy.post.calls.count()).toBe(1);

  });

  it('should delete an email',(done: DoneFn)=>{
    let emailToDelete:Email = new Email('1','1','whatever@gmail.com');

    httpSpy.delete.and.nextWith(new HttpResponse ({
      status: 200
    }));

    service = TestBed.inject(EmailService);
    service.deleteEmail(emailToDelete).subscribe( response => {
      expect(response.status).toEqual(200);
      done();
    });

    expect(httpSpy.delete.calls.count()).toBe(1);
  });
});

function MyHandleError<T>(operation?: string, result?: T): (error: HttpErrorResponse) => Observable<T> {
  return (error: HttpErrorResponse): Observable<T> => {
    return of (result as T);
  };

}
