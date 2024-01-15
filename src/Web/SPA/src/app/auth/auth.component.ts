import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { BreakpointClassDirective } from '../directives/breakpointClass.directive';
@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatSelectModule,
    MatButtonModule,
    MatDividerModule,
    BreakpointClassDirective,
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
})
export class AuthComponent implements OnInit {
  constructor(public breakpointObserver: BreakpointObserver) {}
  screenSize = 'lg';
  ngOnInit(): void {
    this.breakpointObserver
      .observe([
        Breakpoints.XSmall,
        Breakpoints.Medium,
        Breakpoints.Small,
        Breakpoints.Large,
        Breakpoints.XLarge,
      ])
      .subscribe((result) => {
        if (result.matches) {
          // The screen is matching one of the specified breakpoints
          if (result.breakpoints[Breakpoints.XSmall]) {
            this.screenSize = 'xs';
          }
          if (result.breakpoints[Breakpoints.Medium]) {
            this.screenSize = 'md';
          }
          if (result.breakpoints[Breakpoints.Large]) {
            this.screenSize = 'lg';
          }
          if (result.breakpoints[Breakpoints.Small]) {
            this.screenSize = 'sm';
          }
          if (result.breakpoints[Breakpoints.XLarge]) {
            this.screenSize = 'xl';
          }
        }
      });
  }

  authForm = new FormGroup({
    phoneNumber: new FormControl(''),
    countryCode: new FormControl('84'),
  });
}
