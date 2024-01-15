import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import {
  FullscreenOverlayContainer,
  OverlayContainer,
  OverlayModule,
} from '@angular/cdk/overlay';
import { CommonModule, DatePipe } from '@angular/common';
import {
  Component,
  ElementRef,
  HostListener,
  OnInit,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { DatePickerComponent } from '../date/datepicker.component';
import { TimeRangeTransformPipe } from '../../pipes/time-range-transform.pipe';
type Refer = {
  label: string;
  imageUrl: string;
};
const refers: Refer[] = [
  {
    label: "I'm flexible",
    imageUrl:
      'https://a0.muscache.com/pictures/f9ec8a23-ed44-420b-83e5-10ff1f071a13.jpg',
  },
  {
    label: 'Europe',
    imageUrl:
      'https://a0.muscache.com/im/pictures/7b5cf816-6c16-49f8-99e5-cbc4adfd97e2.jpg?im_w=320',
  },
  {
    label: 'South Korea',
    imageUrl:
      'https://a0.muscache.com/im/pictures/c193e77c-0b2b-4f76-8101-b869348d8fc4.jpg?im_w=320',
  },
  {
    label: 'Australia',
    imageUrl:
      'https://a0.muscache.com/im/pictures/42a1fb0f-214c-41ec-b9d7-135fbbdb8316.jpg?im_w=320',
  },
  {
    label: 'Japan',
    imageUrl:
      'https://a0.muscache.com/im/pictures/26891a81-b9db-4a9c-8aab-63486b7e627c.jpg?im_w=320',
  },
];

@Component({
  selector: 'app-toolbar',
  standalone: true,
  imports: [
    MatButtonModule,
    MatToolbarModule,
    DatePickerComponent,
    OverlayModule,
    CommonModule,
    MatIconModule,
  ],
  templateUrl: './app-toolbar.component.html',
  styleUrl: './app-toolbar.component.scss',
  providers: [
    { provide: OverlayContainer, useClass: FullscreenOverlayContainer },
  ],
})
export class AppToolbarComponent implements OnInit {
  @ViewChild('subnavViewport') subnavViewport?: ElementRef;
  @ViewChild('subnavList')
  subnavList?: ElementRef;
  isMediumViewDown: boolean = false;
  labels: Label[] = [];
  activeLabelId: string = '';
  isLoading: boolean = false;
  constructor(
    private dialog: MatDialog,
    private renderer: Renderer2,
    private _comomApi: CommonService,
    private router: Router,
    private breakpointObserver: BreakpointObserver
  ) {}
  isScrollDown = false;
  @HostListener('mousewheel', ['$event'])
  @HostListener('DOMMouseScroll', ['$event'])
  handleScroll(event: WheelEvent): void {
    this.isScrollDown = event.deltaY > 0 ? true : false;
  }
  translationX = 0;
  onLabelClick(id: string) {
    this.router.navigate(['/property'], { queryParams: { labelId: id } });
    this.activeLabelId = id;
  }
  onNextClick = () => {
    const subnavListElement = this.subnavList?.nativeElement;
    if (
      this.subnavList?.nativeElement.getBoundingClientRect().right -
        this.subnavViewport?.nativeElement.getBoundingClientRect().right <=
      150
    ) {
      this.translationX -=
        this.subnavList?.nativeElement.getBoundingClientRect().right -
        this.subnavViewport?.nativeElement.getBoundingClientRect().right;
      this.renderer.setStyle(
        subnavListElement,
        'transform',
        `translateX(${this.translationX}px)`
      );
      this.translationX = 0;
    } else {
      this.translationX -= 150;
      this.renderer.setStyle(
        subnavListElement,
        'transform',
        `translateX(${this.translationX}px)`
      );
    }
  };
  onPreviousClick = () => {
    const subnavListElement = this.subnavList?.nativeElement;
    this.translationX += 150; // You can adjust the amount to translate
    // Apply the new translation
    this.renderer.setStyle(
      subnavListElement,
      'transform',
      `translateX(${this.translationX}px)`
    );
  };
  onClose = () => {
    this.dialog.closeAll();
  };
  activeField: string = 'where';
  ngOnInit() {
    this.isLoading = true;
    this._comomApi.getAllLabels().subscribe((value) => {
      this.labels = value;
      this.isLoading = false;
    });

    this.breakpointObserver
      .observe([Breakpoints.Small, Breakpoints.XSmall])
      .subscribe((value) => {
        this.isMediumViewDown = value.matches;
      });
  }

  openDialog() {
    this.dialog.open(HeaderDialogComponent, {
      width: '100%',
      height: '100%',
      maxHeight: '100%',
      maxWidth: '100%',
    });
  }
}

import { FormsModule } from '@angular/forms';
import { DateRange } from '@angular/material/datepicker';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { Label } from '../../Models';
import { CommonService } from '../../services/common.service';
@Component({
  selector: 'app-header-dialog',
  standalone: true,
  imports: [
    CommonModule,
    DatePickerComponent,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatButtonModule,
    TimeRangeTransformPipe,
    FormsModule,
  ],
  templateUrl: './header-dialog.component.html',
  styleUrl: './header-dialog.component.scss',
  providers: [TimeRangeTransformPipe, DatePipe],
})
export class HeaderDialogComponent {
  guestNumber = {
    adult: 0,
    children: 0,
    pet: 0,
    infants: 0,
  };
  favoriteColor = '';
  constructor(public timeRangeTransformPipe: TimeRangeTransformPipe) {}
  selectedDateRange: string | null = null;
  onSelectDestination(label: string) {
    this.destination = label;
    this.activeField = 'when';
  }
  handleChangeQuantityOfQuest(
    field: 'adult' | 'children' | 'pet' | 'infants',
    value: number
  ) {
    this.guestNumber = {
      ...this.guestNumber,
      [field]: this.guestNumber[field] + value,
    };
  }
  handleDateRangeChange(dateRange: DateRange<Date>) {
    this.selectedDateRange = this.timeRangeTransformPipe.transform(dateRange);
    console.log(dateRange);
  }
  rcms = refers;
  activeField: string = 'where';
  destination: string = '';
  onExpension(fieldName: 'where' | 'wheen' | 'who') {
    this.activeField = fieldName;
  }
}
