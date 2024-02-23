import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocationService } from './location.service';
import { Person } from '../models/person';
import { Email } from '../models/email';
import { Observable, catchError } from 'rxjs';
import { ErrorHandlerService, HandleError } from './errorHandler.service';
import { IEmail } from '../Interfaces/iemail';

@Injectable({
  providedIn: 'root'
})
export class EmailService {

  private emailServiceLocation: string;
  private handleError: HandleError;


    public constructor(private http: HttpClient, private configService: LocationService,httpErrorHandler: ErrorHandlerService) {
        this.emailServiceLocation = this.configService.getLocationUrl() + 'PersonalEmail/';
        this.handleError = httpErrorHandler.createHandleError('EmailService');

    }

    //get the list of email addresses for the person
    public getEmailAddresses(person: Person): Observable<IEmail[]> {
        var requestUrl: string = this.emailServiceLocation + person.id;
        return this.http.get<IEmail[]>(requestUrl)
        .pipe(
          catchError(this.handleError('getEmailAddresses', [])));
    }

    //save the email
    public addEmail(email: Email){
      const request ={personId:email.personId,address:email.address}
        return this.http.post(this.emailServiceLocation, request).pipe(
            catchError(this.handleError('saveEmail')));
    }

    //delete the email
    public deleteEmail(email: Email): Observable<any> {
        var requestUrl: string = this.emailServiceLocation + email.id;
        return this.http.delete(requestUrl).pipe(
          catchError(this.handleError('deleteEmail')));
    }

}
