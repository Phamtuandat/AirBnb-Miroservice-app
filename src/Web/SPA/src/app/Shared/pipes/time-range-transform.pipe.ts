import { DatePipe } from '@angular/common';
import { Injectable, Pipe, PipeTransform } from '@angular/core';
import { DateRange } from '@angular/material/datepicker';

@Pipe({
  name: 'timerange',
  standalone: true,
})
export class TimeRangeTransformPipe implements PipeTransform {
  constructor(private datePipe: DatePipe) {}
  start: string | null = '';
  end: string | null = '';
  transform(value: DateRange<Date>): string {
    if (value.start?.getFullYear() === new Date().getFullYear()) {
      this.start = this.datePipe.transform(value.start, 'MMM d');
    } else {
      this.start = this.datePipe.transform(value.start, 'MMM d, YYY');
    }

    if (
      value.end?.getFullYear() === value.start?.getFullYear() &&
      value.end?.getMonth() === value.start?.getMonth()
    ) {
      this.end = this.datePipe.transform(value.end, 'd');
    } else if (
      value.end?.getFullYear() === value.start?.getFullYear() &&
      value.end?.getMonth() !== value.start?.getMonth()
    ) {
      this.end = this.datePipe.transform(value.end, 'MMM d');
    } else {
      this.end = this.datePipe.transform(value.end, 'MMM d, YYYY');
    }

    return `${this.start} - ${this.end}`;
  }
}
