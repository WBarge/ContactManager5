import { Person } from './person';

describe('Person', () => {
  it('should create an instance', () => {
    expect(Person.CreateEmpty()).toBeTruthy();
  });
});
