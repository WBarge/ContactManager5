

export class Phone {
  public id: string;
  public personId:string;
  public areaCode: string;
  public countryCode: string;
  public number: string;
  public phoneType: string;

  constructor (id:string,personId:string,areaCode:string,countryCode:string,number:string,phoneType:string) {
    this.id = id;
    this.personId = personId;
    this.areaCode = areaCode;
    this.countryCode = countryCode;
    this.number = number;
    this.phoneType = phoneType;
  }

  public get formattedNumber(): string {
    var returnValue: string = '';
    if (this.countryCode !== '') {
        returnValue = this.countryCode + '-' + this.areaCode + '-' + this.formatMainNumber();
    }
    else if (this.areaCode !== '') {
        returnValue = '(' + this.areaCode + ')' + this.formatMainNumber();
    }
    else {
        returnValue = this.formatMainNumber();
    }
    return returnValue;
  }

  private formatMainNumber(): string {

    var result: string = '';
    for (var sub = 0, length = this.number.length; sub < length; sub++){
        var strPart = this.number[sub];
        if (this.IsNumeric(strPart)) {
            result += strPart;
        }
    }
    var returnValue: string = '';
    if (result !== '') {
        returnValue = result.substring(0, 3) + '-' + result.substring(3);
    }

    return returnValue;
  }

  public static CreateEmpty():Phone{
    return new Phone('','','','','','');
  }

  private IsNumeric(str:string):boolean{
    return !isNaN(Number(str));
  }
}
