import { CommonModule, DatePipe } from '@angular/common';
import { Component, ElementRef, Input, Renderer2 } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { DateRange } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { Property } from '../../Shared/Models';
import { AppToolbarComponent } from '../../Shared/components/header/app-toolbar.component';
import { TimeRangeTransformPipe } from '../../Shared/pipes/time-range-transform.pipe';
import { BreakpointClassDirective } from '../../directives/breakpointClass.directive';

@Component({
  selector: 'app-property-card',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    BreakpointClassDirective,
    RouterModule,
  ],
  templateUrl: './property-card.component.html',
  styleUrl: './property-card.component.scss',
  providers: [TimeRangeTransformPipe, DatePipe],
})
export class PropertyCardComponent {
  constructor(
    private renderer: Renderer2,
    private el: ElementRef,
    public timeRangeTransformPipe: TimeRangeTransformPipe
  ) {}
  @Input() property?: Property;
  translateValue: number = 0;
  activeImgIdx: number = 0;
  testDots = new Array(10);
  availableTime: DateRange<Date> = new DateRange(
    new Date(2024, 0, 1),
    new Date(2024, 0, 12)
  );
  isMouseenter: boolean = false;
  onPreviousImageClick = () => {
    if (this.property?.medias) {
      if (this.translateValue > 100) {
        this.translateValue = 0;
        this.activeImgIdx -= 1;
        return;
      }
      this.translateValue += 100; // Assuming each slide is 100% wide
      this.activeImgIdx -= 1;
    }
  };
  handleMouseEnter(value: boolean): void {
    this.isMouseenter = value;
  }
  onNextImgClick = () => {
    if (this.property?.medias) {
      this.translateValue -= 100;
      this.activeImgIdx = this.activeImgIdx + 1;
      if (this.translateValue < -100 * (this.property.medias.length - 1)) {
        this.translateValue = 0;
        this.activeImgIdx = 0;
      }
    }
  };
  handleDateRange() {
    return this.timeRangeTransformPipe.transform(this.availableTime);
  }
}
