import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_NATIVE_DATE_FORMATS,
  MatNativeDateModule,
  NativeDateAdapter,
  NativeDateModule,
} from '@angular/material/core';
import { DateRange, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-datepicker',
  standalone: true,
  templateUrl: './datepicker.component.html',
  styleUrl: './datepicker.component.scss',
  imports: [
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FormsModule,
    ReactiveFormsModule,
    NativeDateModule,
  ],
  providers: [
    { provide: DateAdapter, useClass: NativeDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: MAT_NATIVE_DATE_FORMATS },
  ],
})
export class DatePickerComponent {
  @Output() onDateChange = new EventEmitter();
  selectedDateRange: DateRange<Date> = new DateRange(new Date(), new Date());
  dateFilter = (d: Date) => d >= new Date();
  _onSelectedChange(date: Date | null): void {
    if (
      date &&
      this.selectedDateRange &&
      this.selectedDateRange.start &&
      date > this.selectedDateRange.start &&
      !this.selectedDateRange.end
    ) {
      this.selectedDateRange = new DateRange(
        this.selectedDateRange.start,
        date
      );
      this.onDateChange.emit(this.selectedDateRange);
    } else {
      this.selectedDateRange = new DateRange(date, null);
      this.onDateChange.emit(this.selectedDateRange);
    }
  }
}
