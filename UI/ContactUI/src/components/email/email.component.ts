import { Component, Input, OnInit  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IEmail } from '../../Interfaces/iemail';
import { Email } from '../../models/email';
import {InputTextModule} from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-email',
  standalone: true,
  imports: [InputTextModule,FormsModule,CommonModule],
  templateUrl: './email.component.html',
  styleUrl: './email.component.css'
})
export class EmailComponent implements OnInit {

  @Input('EmailData') data!: IEmail;

  ngOnInit(): void {
    if (this.data === null){
      this.data = Email.CreateEmpty();
    }
  }
}
