import { NgModule } from '@angular/core';
import { Person } from "../person";

NgModule({
  imports:[
    Person
  ]
})
export module PersonComparer {

  export function AreTheSame(oldObj: Person, newObj: Person): boolean {
    var returnValue: boolean = true;

    if (oldObj.id != newObj.id ||
        oldObj.firstName != newObj.firstName ||
        oldObj.lastName != newObj.lastName) {
        returnValue = false;
    }
    return returnValue;
  }
}

