import { Phone } from './phone';

describe('Phone', () => {
  it('should create an instance', () => {
    expect(Phone.CreateEmpty()).toBeTruthy();
  });
});
