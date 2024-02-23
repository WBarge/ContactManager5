import { TestBed } from '@angular/core/testing';

import { PhoneService } from './phone.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Spy, createSpyFromClass } from 'jasmine-auto-spies';
import { IPhone } from '../Interfaces/iphone';
import { ErrorHandlerService, HandleError } from './errorHandler.service';
import { MessageService } from 'primeng/api';
import { Observable, of } from 'rxjs';
import { HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { Person } from '../models/person';
import { LocationService } from './location.service';

describe('PhoneService', () => {
  let service: PhoneService;
  let httpSpy: Spy<HttpClient>;
  let errorHandler:Spy<ErrorHandlerService>;
  let messageService:Spy<MessageService>;
  let configService:Spy<LocationService>;

  let testData:IPhone[] = [
    {
      id: '1',
      personId: '',
      countryCode: '1',
      areaCode: '714',
      number: '555-4324',
      phoneType: 'cell',
      deleted: false
    },
    {
      id: '2',
      personId: '',
      countryCode: '1',
      areaCode: '213',
      number: '555-4554',
      phoneType: 'home',
      deleted: false
    }
  ]

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers:[
      {provide:HttpClient, useValue: createSpyFromClass(HttpClient)},
      {provide:ErrorHandlerService, useValue: createSpyFromClass(ErrorHandlerService)},
      {provide:MessageService, useValue: createSpyFromClass(MessageService)},
      {provide:LocationService, useValue: createSpyFromClass(LocationService)},
      PhoneService
    ]
    });
    messageService = TestBed.inject<any>(MessageService);
    errorHandler = TestBed.inject<any>(ErrorHandlerService);
    errorHandler.createHandleError = MyHandleError as any;
    configService = TestBed.inject<any>(LocationService);
    httpSpy = TestBed.inject<any>(HttpClient);
  });

  it('should be created', () => {
    configService.getLocationUrl.and.returnValue("someLocation");
    service = TestBed.inject(PhoneService);
    expect(service).toBeTruthy();
    expect(configService.getLocationUrl.calls.count()).toBe(1);

  });

  it('getPhoneNumbers should return data', () => {
    httpSpy.get.and.nextWith(testData);

    service = TestBed.inject(PhoneService);
    service.getPhoneNumbers(new Person('12','test','test',false))
    .subscribe( (phones:IPhone[])=>{
        expect(phones).toHaveSize(testData.length);
      }
    );

    expect(httpSpy.get.calls.count()).toBe(1);
  });
});

function MyHandleError<T>(operation?: string, result?: T): (error: HttpErrorResponse) => Observable<T> {
  return (error: HttpErrorResponse): Observable<T> => {
    return of (result as T);
  };

}



