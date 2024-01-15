import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Directive, ElementRef, Input, OnInit, Renderer2 } from '@angular/core';

@Directive({
  selector: '[breakpointClass]',
  standalone: true,
})
export class BreakpointClassDirective implements OnInit {
  @Input() breakpointClass: string = '';
  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2,
    private breakpointObserver: BreakpointObserver
  ) {}

  ngOnInit(): void {
    this.breakpointObserver
      .observe([
        Breakpoints.Small,
        Breakpoints.XSmall,
        Breakpoints.Medium,
        Breakpoints.XLarge,
        Breakpoints.Large,
      ])
      .subscribe((result) => {
        this.applyClasses(result);
      });
  }
  private applyClasses(result: any): void {
    const classesToRemove = [
      `${this.breakpointClass}-xs`,
      `${this.breakpointClass}-sm`,
      `${this.breakpointClass}-md`,
      `${this.breakpointClass}-lg`,
      `${this.breakpointClass}-xl`,
    ];

    for (const className of classesToRemove) {
      this.renderer.removeClass(this.elementRef.nativeElement, className);
    }

    if (result.matches) {
      if (result.breakpoints[Breakpoints.XSmall]) {
        this.renderer.addClass(
          this.elementRef.nativeElement,
          `${this.breakpointClass}-xs`
        );
      }
      if (result.breakpoints[Breakpoints.Small]) {
        this.renderer.addClass(
          this.elementRef.nativeElement,
          `${this.breakpointClass}-sm`
        );
      }
      if (result.breakpoints[Breakpoints.Medium]) {
        this.renderer.addClass(
          this.elementRef.nativeElement,
          `${this.breakpointClass}-md`
        );
      }
      if (result.breakpoints[Breakpoints.Large]) {
        this.renderer.addClass(
          this.elementRef.nativeElement,
          `${this.breakpointClass}-lg`
        );
      }
      if (result.breakpoints[Breakpoints.XLarge]) {
        this.renderer.addClass(
          this.elementRef.nativeElement,
          `${this.breakpointClass}-xl`
        );
      }
    }
  }
}
