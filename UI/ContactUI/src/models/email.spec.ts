import { Email } from './email';

describe('Email', () => {
  it('should create an instance', () => {
    expect( Email.CreateEmpty()).toBeTruthy();
  });
});
