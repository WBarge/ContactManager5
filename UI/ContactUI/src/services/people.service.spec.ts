import { TestBed } from '@angular/core/testing';

import { PeopleService } from './people.service';
import { Spy, createSpyFromClass } from 'jasmine-auto-spies';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ErrorHandlerService } from './errorHandler.service';
import { MessageService } from 'primeng/api';
import { LocationService } from './location.service';
import { Observable, of } from 'rxjs';
import { IPerson } from '../Interfaces/iperson';
import { Person } from '../models/person';

describe('PeopleService', () => {
  let service: PeopleService;
  let httpSpy: Spy<HttpClient>;
  let errorHandler:Spy<ErrorHandlerService>;
  let configService:Spy<LocationService>;
  let testLocation:string = 'someLocation';

  beforeEach(() => {

    TestBed.configureTestingModule({
      providers:[
      {provide:HttpClient, useValue: createSpyFromClass(HttpClient)},
      {provide:ErrorHandlerService, useValue: createSpyFromClass(ErrorHandlerService)},
      {provide:MessageService, useValue: createSpyFromClass(MessageService)},
      {provide:LocationService, useValue: createSpyFromClass(LocationService)},
      PeopleService
    ]
    });
    errorHandler = TestBed.inject<any>(ErrorHandlerService);
    errorHandler.createHandleError = MyHandleError as any;
    configService = TestBed.inject<any>(LocationService);
    configService.getLocationUrl.and.returnValue(testLocation);
    httpSpy = TestBed.inject<any>(HttpClient);

  });

  it('should be created', () => {
    service = TestBed.inject(PeopleService);
    expect(service).toBeTruthy();
    expect(configService.getLocationUrl.calls.count()).toBeGreaterThanOrEqual(2);
  });

  it('should get all people', () => {

      let testData:IPerson[] = [
        {
          id: '1',
          firstName:'foo',
          lastName:'bar',
          deleted:false
        },
        {
          id: '2',
          firstName:'test',
          lastName:'person',
          deleted:false
        }
      ]

      httpSpy.get.and.nextWith(testData);

      service = TestBed.inject(PeopleService);
      service.getPeople()
      .subscribe( (people:IPerson[])=>{
          expect(people).toHaveSize(testData.length);
        }
      );

      expect(httpSpy.get.calls.count()).toBe(1);
    });

    it('should save a person',(done: DoneFn)=>{

      let personToinsert:Person = new Person('','foo','bar',false);

      httpSpy.post.and.nextWith(new HttpResponse ({
        status: 200
      }));

      service = TestBed.inject(PeopleService);
      service.savePerson(personToinsert).subscribe(response => {
        expect(response.status).toEqual(200);
        done();
      });

      expect(httpSpy.post.calls.count()).toBe(1);

    });

});

function MyHandleError<T>(operation?: string, result?: T): (error: HttpErrorResponse) => Observable<T> {
  return (error: HttpErrorResponse): Observable<T> => {
    return of (result as T);
  };

}
