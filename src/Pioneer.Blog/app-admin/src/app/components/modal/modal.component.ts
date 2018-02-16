import { Component, Input } from '@angular/core';

@Component({
  selector: 'pc-modal',
  template: `
  <div class="pc-modal" *ngIf="show">
    <div class="pc-modal-overlay" (click)="close()"></div>
    <div class="pc-modal-dialog" ng-style="dialogStyle" [style.width]="width">
      <div class="pc-modal-dialog-content">
        <ng-content></ng-content>
      </div>
    </div>
  </div>
  `,
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {
  @Input() width = '50%';
  private show = false;

  close(): void {
    this.show = false;
  }

  open(): void {
    this.show = true;
  }
}
