import { Component,OnInit } from '@angular/core';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { PeopleService } from '../../services/people.service';
import { Person } from '../../models/person';
import { IPerson } from '../../Interfaces/iperson';
import { map } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { PersonComponent } from '../../components/person/person.component';
import { PersonComparer } from '../../models/comparers/person-comparer';
import { ContactDetailsComponent } from "../../components/contact-details/contact-details.component";



@Component({
    selector: 'app-person-list',
    standalone: true,
    imports: [ToastModule, TableModule, ButtonModule, DialogModule, PersonComponent, ContactDetailsComponent],
    providers: [PeopleService],
    templateUrl: './person-list.component.html',
    styleUrl: './person-list.component.css',
})
export class PersonListComponent implements OnInit {

  //data for the grid
  personData!: Person[];

  //edit person dialog
  showPersonDetailDialog: boolean;
  orginalPersonObject!: Person | null;

  //used in the edit and the detail
  selectedPerson!: Person |null;

  //create new person dialog
  showNewPersonDialog: boolean;
  newPerson!: Person | null;

  //configure the contact details
  showContactDetailsDialog: boolean;

  constructor(private messageService:MessageService,private dataService:PeopleService){
    this.showPersonDetailDialog = false;
    this.showNewPersonDialog = false;
    this.showContactDetailsDialog = false;
    this.resetObjectTrackers()
  }

  public ngOnInit(): void {
      this.loadPeople();
  }

  public clickShowDetailDialogButton(person: Person) {
    this.selectedPerson = person;
    this.orginalPersonObject = person.Clone();
    this.showPersonDetailDialog = true;
  }
  public clickSavePersonDetailDialogButton() {
    const personToUpload: Person = this.selectedPerson as Person;
    this.dataService.savePerson(personToUpload).subscribe(
        () => { this.sendMessage('info','System Message', `${personToUpload.formattedName} was saved`); });
    this.showPersonDetailDialog = false;
  }
  public clickClosePersonDetailDialogButton() {
    if (PersonComparer.AreTheSame(this.orginalPersonObject as Person, this.selectedPerson as Person) == false) {
        this.orginalPersonObject!.CopyTo(this.selectedPerson as Person);
    }
    this.showPersonDetailDialog = false;
    this.resetObjectTrackers();
  }


  public clickShowPersonAdditionalInformationDialogButton(person: Person) {
    this.selectedPerson = person;
    this.showContactDetailsDialog = true;
  }

  public clickCloseContactDetailsDialogButton() {
    //we need to set the selected person to null so if the user clicks clogs for the same person in the grid again
    //the change system will activate and the detail data for the person will be loaded from the db again
    //this allows us to see changes made by other users without having to click a different person and back again
    this.selectedPerson = null;
    this.showContactDetailsDialog = false;
}

  public clickShowNewPersonDialogButton() {
    if (this.newPerson == null){
      this.newPerson = Person.CreateEmpty();
    }
    this.showNewPersonDialog = true;
  }

  public clickCreateNewPersonDialogButton() {
    const personToUpload: Person = this.newPerson as Person;
    this.dataService.savePerson(personToUpload).subscribe(   () => {
          this.sendMessage('info','System Message', `${personToUpload.formattedName} was created`);
          this.loadPeople()});
    this.showNewPersonDialog = false;
}


  private loadPeople() {
    this.sendMessage('info','System Message','Loading People');
    this.dataService.getPeople()
    .pipe(map((people:IPerson[])=>{
      const returnValue:Person[] = new Array();
      people.forEach((person:IPerson)=>{
        returnValue.push(new Person(person.id,person.firstName,person.lastName,person.deleted));
      } );
      return returnValue;
    }
      )) .subscribe((people:Person[])=>{
        this.personData=people;
        this.resetObjectTrackers();
      });
  }

  private resetObjectTrackers() {
    this.orginalPersonObject = null;
    this.selectedPerson = null;
    this.newPerson = null;
  }

  private sendMessage(severity:string,summary:string,detail:string){
    this.messageService.add({severity:severity,summary:summary,detail:detail});
  }

}
