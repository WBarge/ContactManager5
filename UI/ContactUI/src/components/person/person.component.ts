import { Component,OnInit,Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Person } from '../../models/person';
import {InputTextModule} from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-person',
  standalone: true,
  imports: [InputTextModule,FormsModule,CommonModule],
  templateUrl: './person.component.html',
  styleUrl: './person.component.css'
})
export class PersonComponent implements OnInit {

  @Input('PersonData') data!:Person|null;

  ngOnInit(): void {
    if (this.data === null){
      this.data = Person.CreateEmpty();
    }
  }

}
