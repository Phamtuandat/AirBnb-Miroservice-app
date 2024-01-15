import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppLayoutComponent } from './app-layout/app-layout.component';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    AppLayoutComponent,
    MatDatepickerModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'BnbApp';
}
