import { Component, Input } from '@angular/core';

@Component({
  selector: 'pc-loader',
  styles: [
    'loader.component.scss'
  ],
  template: `
  <section class="pc-loader" *ngIf="loading">
    <div class="pc-loader-overlay"></div>
    <div class="pc-loader-wrapper">
      <i class="fa fa-spinner fa-pulse fa-fw"></i>
    </div>
  </section>
  `
})

export class LoaderComponent {
  @Input() loading = false;
}
