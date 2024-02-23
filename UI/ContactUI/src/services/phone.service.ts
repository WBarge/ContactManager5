import { Injectable } from '@angular/core';
import { LocationService } from './location.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ErrorHandlerService, HandleError } from './errorHandler.service';
import { Person } from '../models/person';
import { IPhone } from '../Interfaces/iphone';
import { Observable, catchError, map } from 'rxjs';
import { Phone } from '../models/phone';

@Injectable({
  providedIn: 'root'
})
export class PhoneService {

  private phoneServiceLocation: string;
  private handleError: HandleError;


  public constructor(private http: HttpClient, private configService: LocationService,httpErrorHandler: ErrorHandlerService) {
      this.phoneServiceLocation = this.configService.getLocationUrl() + 'PersonalPhoneNumber/';
      this.handleError = httpErrorHandler.createHandleError('PhoneService');
  }

  //get the phone numbers for the person
  public getPhoneNumberss(person: Person): Observable<IPhone[]> {
    var requestUrl: string = this.phoneServiceLocation + person.id;
    return this.http.get<IPhone[]>(requestUrl)
    .pipe(
      map((phoneNumbers:IPhone[])=>{
        phoneNumbers.forEach((number:IPhone)=>number.personId = person.id);
        return phoneNumbers;
      }),
      catchError(this.handleError('getPhoneNumberss', [])));
  }

  //save the phone number
  public addPhoneNumber(phone: Phone) {
    var request = {personId:phone.personId,
      countryCode:phone.countryCode,
      areaCode:phone.areaCode,
      number:phone.number,
      phoneType:phone.phoneType
    };

    return this.http.post(this.phoneServiceLocation, request)
      .pipe(
        catchError(this.handleError('addPhoneNumber')));
  }

  public deletePhoneNumber(phone:Phone){
    var requestUrl: string = this.phoneServiceLocation;
        var paras: HttpParams = new HttpParams()
        .set('phoneId', phone.id)
        .set('personId', phone.personId);
        return this.http.delete(requestUrl, { params:paras })
        .pipe(
          catchError(this.handleError('deletePhoneNumber')));
  }
}
