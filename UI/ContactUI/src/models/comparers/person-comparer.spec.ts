import { Person } from '../person';
import { PersonComparer } from './person-comparer';

describe('PersonComparer', () => {

  let pOne:Person;
  let pTwo:Person;

  beforeEach(() => {
     pOne = Person.CreateEmpty();
     pTwo = Person.CreateEmpty();

    });

  it('should have two people as the same',()=>{
    expect(PersonComparer.AreTheSame(pOne,pTwo)).toBeTrue();
  });

  it('should have two people as different',()=>{
    pTwo.firstName = "what";
    expect(PersonComparer.AreTheSame(pOne,pTwo)).toBeFalse();
  });

});
