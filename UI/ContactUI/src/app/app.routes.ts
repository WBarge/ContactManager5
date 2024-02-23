import { Routes } from '@angular/router';
import { AboutComponent } from '../pages/about/about.component';
import { PersonListComponent } from '../pages/personList/person-list.component';

export const routes: Routes = [
  { path:'',redirectTo:'/contacts',pathMatch:'full'},
  { path:'about',component: AboutComponent},
  { path:'contacts',component: PersonListComponent}
];
