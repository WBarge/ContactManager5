import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Person } from '../../models/person';
import { AccordionModule } from 'primeng/accordion';
import { Table, TableModule } from 'primeng/table';
import {InputTextModule} from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { Email } from '../../models/email';
import { EmailService } from '../../services/email.service';
import { PhoneService } from '../../services/phone.service';
import { MessageService } from 'primeng/api';
import { IEmail } from '../../Interfaces/iemail';
import { map } from 'rxjs';
import { Phone } from '../../models/phone';
import { IPhone } from '../../Interfaces/iphone';
import { IPhoneType } from '../../Interfaces/iphone-type';

@Component({
  selector: 'app-contact-details',
  standalone: true,
  imports: [AccordionModule,TableModule,CommonModule,InputTextModule,FormsModule,ButtonModule,DropdownModule,InputMaskModule],
  providers:[EmailService,PhoneService],
  templateUrl: './contact-details.component.html',
  styleUrl: './contact-details.component.css'
})
export class ContactDetailsComponent implements OnChanges,OnInit {


  @Input('PersonData') data!: Person|null;

  emails:Email[] = new Array();
  phoneNumbers: Phone[] = new Array();
  phoneTypes:IPhoneType[] = new Array();


  constructor(private messageService:MessageService,private emailService:EmailService,private phoneService:PhoneService){

  }

  public ngOnInit(): void {
    this.loadPhoneTypes();//The phone types are static
  }
  loadPhoneTypes() {
    this.phoneTypes = [
      {name:"Home",code:"home"},
      {name:"Mobile",code:"cell"},
      {name:"Work",code:"work"}
    ];
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (this.data !== null) {
      this.loadEmailAddresses();
      this.loadPhoneNumbers();
    }
  }

  private loadEmailAddresses() {
    this.emailService.getEmailAddresses(this.data as Person)
    .pipe(
      map((emails:IEmail[])=>{
        const returnValue:Email[] = new Array();
        emails.forEach((email:IEmail)=>{
          returnValue.push(new Email(email.id,email.personId,email.address));
        });
        return returnValue;
      })
    )
    .subscribe((emailAddresses: Email[]) => {
        this.emails = emailAddresses;
    });
  }

  public clickAddNewEmailButton(emailTable: Table) {
    let newEmail:Email =  Email.CreateEmpty();
    newEmail.personId = (this.data as Person).id;
    this.emails.push(newEmail);
    emailTable.value = this.emails;
    emailTable.reset();
  }

  public handleEmailEditComplete(event: any) {
    this.saveEmail(event.data);
  }

  private saveEmail(email: Email) {
    email.personId = this.data?.id as string;
    this.emailService.addEmail(email).subscribe(
        () => {
          this.loadEmailAddresses()
          this.sendMessage('info','',`${email.address} was saved for ${(this.data as Person).formattedName}`);
        });
  }

  public clickDeleteEmailButton(email: Email) {
    this.emailService.deleteEmail(email).subscribe(
        () => {
          this.loadEmailAddresses()
          this.sendMessage('info','',`${email.address} was removed from ${(this.data as Person).formattedName}`);
        });

}

private loadPhoneNumbers() {
  this.phoneService.getPhoneNumbers(this.data as Person)
  .pipe(
    map((numbers:IPhone[])=>{
      const returnValue:Phone[] = new Array();
      numbers.forEach((phoneNumber:IPhone)=>{
        returnValue.push(new Phone(phoneNumber.id,phoneNumber.personId,phoneNumber.areaCode,phoneNumber.countryCode,phoneNumber.number,phoneNumber.phoneType));
      });
      return returnValue;
    })
  )
  .subscribe((phoneNumbers: Phone[]) => {
    this.phoneNumbers = phoneNumbers
  });
}

public clickAddNewPhoneButton(phoneTable: Table) {
  var newPhone: Phone = Phone.CreateEmpty();
  newPhone.personId = (this.data as Person).id;
  this.phoneNumbers.push(newPhone);
  phoneTable.value = this.phoneNumbers;
  phoneTable.reset();
}

public clickEditPhoneSaveButton(phone: Phone) {
  this.savePhone(phone);
}

public clickEditPhoneCancelButton(phone: Phone) {
  this.phoneNumbers = this.phoneNumbers.filter(p=>p.id!=='');
}

clickDeletePhoneButton(phone: Phone) {
  this.deletephone(phone);
}


private savePhone(phone: Phone) {
  this.phoneService.addPhoneNumber(phone).subscribe(
      () => { this.loadPhoneNumbers() });
}

private deletephone(phone: Phone) {
  this.phoneService.deletePhoneNumber(phone).subscribe(
    ()=>{this.loadPhoneNumbers()});
}


//This is a quick and dirty way to translate the stored phone type to the display value
//another solution would be to move the phone types to a central location and then create a pipe
//to do the translation
public lookupPhoneType(type:string):string{
  let returnValue:string = '';
  this.phoneTypes.forEach((ptype:IPhoneType)=>{
    if(ptype.code===type){
      returnValue = ptype.name;
    }
  });
  return returnValue;
}


  private sendMessage(severity:string,summary:string,detail:string){
    this.messageService.add({severity:severity,summary:summary,detail:detail});
  }
}
