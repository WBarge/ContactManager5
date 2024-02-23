import { Person } from "../person";

export class PersonComparer {

  public static AreTheSame(oldObj: Person, newObj: Person): boolean {
    var returnValue: boolean = true;

    if (oldObj.id != newObj.id ||
        oldObj.firstName != newObj.firstName ||
        oldObj.lastName != newObj.lastName) {
        returnValue = false;
    }
    return returnValue;
}
}
