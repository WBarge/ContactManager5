import { IEmail } from "../Interfaces/iemail";

export class Email implements IEmail {
  public id: string;
  public personId: string;
  public address: string;

  constructor (id:string,personId:string,address:string){
    this.id = id;
    this.personId = personId;
    this.address = address;
  }

  public static CreateEmpty():Email{
    return new Email('','','');
  }

}
