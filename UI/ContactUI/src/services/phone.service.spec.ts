import { TestBed } from '@angular/core/testing';

import { PhoneService } from './phone.service';
import { HttpClient, HttpErrorResponse, HttpParams, HttpResponse } from '@angular/common/http';
import { Spy, createSpyFromClass } from 'jasmine-auto-spies';
import { IPhone } from '../Interfaces/iphone';
import { ErrorHandlerService, HandleError } from './errorHandler.service';
import { MessageService } from 'primeng/api';
import { Observable, of } from 'rxjs';
import { Person } from '../models/person';
import { LocationService } from './location.service';
import { Phone } from '../models/phone';

describe('PhoneService', () => {
  let service: PhoneService;
  let httpSpy: Spy<HttpClient>;
  let errorHandler:Spy<ErrorHandlerService>;
  let configService:Spy<LocationService>;
  let testLocation:string = 'someLocation';
  let locationResult:string = testLocation+'PersonalPhoneNumber/';


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
    errorHandler = TestBed.inject<any>(ErrorHandlerService);
    errorHandler.createHandleError = MyHandleError as any;
    configService = TestBed.inject<any>(LocationService);
    configService.getLocationUrl.and.returnValue(testLocation);
    httpSpy = TestBed.inject<any>(HttpClient);
  });

  it('should be created', () => {
    service = TestBed.inject(PhoneService);
    expect(service).toBeTruthy();
    expect(configService.getLocationUrl.calls.count()).toBe(1);

  });

  it('getPhoneNumbers should return data', () => {

    let testData:IPhone[] = [
      {
        id: '1',
        personId: '5',
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

    httpSpy.get.and.nextWith(testData);

    service = TestBed.inject(PhoneService);
    service.getPhoneNumbers(new Person('12','test','test',false))
    .subscribe( (phones:IPhone[])=>{
        expect(phones).toHaveSize(testData.length);
      }
    );

    expect(httpSpy.get.calls.count()).toBe(1);
  });

  it('should add a phone number',(done: DoneFn)=>{

    let phoneToinsert:Phone = new Phone('1','2','213','1','555-1212','cell');

    httpSpy.post.and.nextWith(new HttpResponse ({
      status: 200
    }));

    service = TestBed.inject(PhoneService);
    service.addPhoneNumber(phoneToinsert).subscribe(response => {
      expect(response.status).toEqual(200);
      done();
    });

    expect(httpSpy.post.calls.count()).toBe(1);

  });

  it('should delete a phone number',(done: DoneFn)=>{
    let phoneToDelete:Phone = new Phone('1','2','213','1','555-1212','cell');

    httpSpy.delete.and.nextWith(new HttpResponse ({
      status: 200
    }));

    service = TestBed.inject(PhoneService);
    service.deletePhoneNumber(phoneToDelete).subscribe( response => {
      expect(response.status).toEqual(200);
      done();
    });

    var paras: HttpParams = new HttpParams()
        .set('phoneId', phoneToDelete.id)
        .set('personId', phoneToDelete.personId);

    expect(httpSpy.delete.calls.count()).toBe(1);
    expect(httpSpy.delete).toHaveBeenCalledWith(locationResult,{params:paras});

  });

});



function MyHandleError<T>(operation?: string, result?: T): (error: HttpErrorResponse) => Observable<T> {
  return (error: HttpErrorResponse): Observable<T> => {
    return of (result as T);
  };

}




