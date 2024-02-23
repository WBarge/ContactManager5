import { IPerson } from "../Interfaces/iperson";


export class Person implements IPerson
{

  constructor (id:string,first:string,last:string,deleted:boolean) {
    this.id = id;
    this.firstName = first;
    this.lastName = last;
    this.deleted = deleted;
  }

  public id: string;
  public firstName: string;
  public lastName: string;
  public deleted: boolean;

  public get formattedName(): string {
    return this.firstName + ' ' + this.lastName;
  }

  public static CreateEmpty():Person  {
    return new Person('','','',false);
  }

  public Clone(): Person {
    var returnValue: Person = new Person(this.id,this.firstName,this.lastName,this.deleted);
    return returnValue;
  }

  public CopyTo(copyToObj: Person) {
      copyToObj.id = this.id;
      copyToObj.firstName = this.firstName;
      copyToObj.lastName = this.lastName;
      copyToObj.deleted = this.deleted;
  }
}
