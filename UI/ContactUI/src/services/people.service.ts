import { Injectable } from '@angular/core';
import { IPerson } from '../Interfaces/iperson';
import { HttpClient } from '@angular/common/http';
import { LocationService } from './location.service';
import { ErrorHandlerService,HandleError } from './errorHandler.service';
import { Observable, catchError } from 'rxjs';
import { Person } from '../models/person';


@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  private peopleServiceLocation:string;
  private personServiceLocation:string;
  private handleError: HandleError;

  constructor(private http: HttpClient,private location:LocationService,httpErrorHandler: ErrorHandlerService) {
      this.peopleServiceLocation = this.location.getLocationUrl()+'People/';
      this.personServiceLocation = this.location.getLocationUrl()+'Person/';
      this.handleError = httpErrorHandler.createHandleError('PeopleService');
   }

  getPeople():Observable<IPerson[]>{
    return this.http.get<IPerson[]>(this.peopleServiceLocation)
    .pipe(
      catchError(this.handleError('getPeople', []))
    );
  }

  savePerson(person:Person):Observable<any>{
    let dataRequest :any;
    if ( person.id ==='')
    {
      dataRequest = {firstName:person.firstName,lastName:person.lastName};
    }
    else
    {
      dataRequest = {id:person.id, firstName:person.firstName,lastName:person.lastName};
    }

    return this.http.post(this.personServiceLocation,dataRequest)
      .pipe(
        catchError(this.handleError('savePerson'))
      )
  }
}
