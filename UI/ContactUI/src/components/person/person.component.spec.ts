import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import {InputTextModule} from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { PersonComponent } from './person.component';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { Person } from '../../models/person';

describe('PersonComponent', () => {
  let component: PersonComponent;
  let fixture: ComponentFixture<PersonComponent>;
  let firstNameInputDE:DebugElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonComponent,FormsModule,InputTextModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonComponent);
    firstNameInputDE = fixture.debugElement.query(By.css('#first-name'));

    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not even show',()=>{
    expect(firstNameInputDE).toBeNull();
  });

  it('should be bond to the input object',fakeAsync(()=>{
    const testPerson:Person = new Person('1','foo','bar',false);
    component.data=testPerson;
    const result = (fixture.nativeElement as HTMLElement).dispatchEvent(new Event('input'));
    fixture.detectChanges();
    fixture.whenStable().then(()=>{
      firstNameInputDE = fixture.debugElement.query(By.css('#first-name'));
      expect(firstNameInputDE.nativeElement.value).toBe(testPerson.firstName);
      });
  }));
});
