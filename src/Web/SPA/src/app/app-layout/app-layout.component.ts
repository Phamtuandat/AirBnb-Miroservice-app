import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCommonModule } from '@angular/material/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterModule } from '@angular/router';
import { AppToolbarComponent } from '../Shared/components/header/app-toolbar.component';
import { BreakpointClassDirective } from '../directives/breakpointClass.directive';
@Component({
  selector: 'app-app-layout',
  standalone: true,
  imports: [
    MatSidenavModule,
    MatCommonModule,
    MatButtonModule,
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    CommonModule,
    MatDialogModule,
    BreakpointClassDirective,
    MatListModule,
    MatGridListModule,
    AppToolbarComponent,
  ],
  templateUrl: './app-layout.component.html',
  styleUrl: './app-layout.component.scss',
})
export class AppLayoutComponent implements OnInit, OnDestroy {
  constructor(private router: Router, public dialog: MatDialog) {}
  ngOnDestroy(): void {}
  ngOnInit(): void {
    this.router.navigate(['/', 'property']);
  }
}
