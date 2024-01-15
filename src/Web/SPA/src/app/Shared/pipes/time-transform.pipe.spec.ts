import { DatePipe } from '@angular/common';
import { TimeRangeTransformPipe } from './time-range-transform.pipe';

describe('TimeTransformPipe', () => {
  let date = new DatePipe('us');
  it('create an instance', () => {
    const pipe = new TimeRangeTransformPipe(date);
    expect(pipe).toBeTruthy();
  });
});
