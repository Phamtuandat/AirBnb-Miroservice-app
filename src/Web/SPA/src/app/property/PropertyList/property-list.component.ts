import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { ActivatedRoute, Params } from '@angular/router';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { Pagination, Property } from '../../Shared/Models';
import { BreakpointClassDirective } from '../../directives/breakpointClass.directive';
import { PropertyCardComponent } from '../property-card/property-card.component';
import { PropertyService } from '../property.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AppLayoutComponent } from '../../app-layout/app-layout.component';

@Component({
  selector: 'app-propertyList',
  standalone: true,
  imports: [
    MatGridListModule,
    BreakpointClassDirective,
    CommonModule,
    PropertyCardComponent,
    InfiniteScrollModule,
    MatButtonModule,
    MatIconModule,
    AppLayoutComponent,
  ],
  templateUrl: './property-list.component.html',
  styleUrl: './property-list.component.scss',
})
export class PropertyListComponent implements OnInit {
  lazyLoadItems = Array(5);
  loadAvailability: boolean = true;
  onScroll() {
    if (
      this.pagination.pageIndex <=
      this.pagination.count / this.pagination.pageSize
    ) {
      this.loadAvailability = false;
      return;
    } else {
      if (this.isLoading || this.loadTimes >= 2) {
        return;
      }
      this.loadData();
    }
  }
  isLoading = false;
  loadTimes: number = 0;
  properties: Property[] = [];
  pagination: Pagination = {
    pageIndex: 0,
    count: 0,
    pageSize: 12,
  };
  activeIdx: number = 0;
  colNumber: number = 4;
  labelId?: string;
  constructor(
    private PropertyService: PropertyService,
    public breakpointObserver: BreakpointObserver,
    public router: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.router.queryParams.subscribe((params: any) => {
      this.labelId = params['labelId'];
      if (this.labelId) {
        this.PropertyService.getPropertiesByLabel(this.labelId).subscribe(
          (response) => {
            this.properties = response.data;
            this.pagination = {
              ...this.pagination,
              pageIndex: response.pageIndex,
              pageSize: response.pageSize,
              count: response.count,
            };
            window.scrollTo(0, 0);
          }
        );
      } else {
        this.PropertyService.getProperties(
          null,
          null,
          null,
          null,
          12
        ).subscribe((value) => {
          this.properties = value.data;
          this.pagination = {
            pageIndex: value.pageIndex,
            count: value.count,
            pageSize: value.pageSize,
          };
        });
      }
    });
    this.breakpointObserver
      .observe([
        '(max-width: 450px)',
        Breakpoints.Small,
        Breakpoints.XSmall,
        Breakpoints.Medium,
        Breakpoints.XLarge,
        Breakpoints.Large,
      ])
      .subscribe((result) => {
        if (result.matches) {
          if (
            result.breakpoints[Breakpoints.XSmall] &&
            !result.breakpoints['(max-width: 450px)']
          ) {
            this.colNumber = 2;
          }
          if (
            result.breakpoints[Breakpoints.XSmall] &&
            result.breakpoints['(max-width: 450px)']
          ) {
            this.colNumber = 1;
          }
          if (result.breakpoints[Breakpoints.Small]) {
            this.colNumber = 2;
          }
          if (result.breakpoints[Breakpoints.Medium]) {
            this.colNumber = 3;
          }
          if (result.breakpoints[Breakpoints.Large]) {
            this.colNumber = 4;
          }
          if (result.breakpoints[Breakpoints.XLarge]) {
            this.colNumber = 4;
          }
        }
      });
  }

  loadData() {
    if (this.labelId) {
      this.isLoading = true;
      this.PropertyService.getPropertiesByLabel(this.labelId).subscribe(
        (value) => {
          this.pagination = {
            ...this.pagination,
            count: value.count,
            pageIndex: value.pageIndex,
            pageSize: value.pageSize,
          };
          this.properties = this.properties.concat(value.data);
          this.isLoading = false;
        }
      );
    } else {
      this.isLoading = true;
      this.PropertyService.getProperties(
        null,
        100000,
        0,
        (this.pagination.pageIndex += 1),
        this.pagination.pageSize
      ).subscribe((response) => {
        this.isLoading = false;
        this.properties = this.properties.concat(...response.data);
        this.pagination = {
          ...this.pagination,
          count: response.count,
          pageIndex: response.pageIndex,
          pageSize: response.pageSize,
        };
        this.isLoading = false;
      });
    }
    this.loadTimes += 1;
  }
}
