import { SystemError } from './system-error';

describe('SystemError POTO ', () => {
  it('should create an instance', () => {
    expect(new SystemError()).toBeTruthy();
  });
});
