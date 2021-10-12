import { Component, OnInit, Input, ChangeDetectionStrategy, Output, EventEmitter } from '@angular/core';
import { InventorySelectable } from '../inventory';

@Component({
  selector: 'app-inventory-detail',
  templateUrl: './inventory-detail.component.html',
  styleUrls: ['./inventory-detail.component.css']
})
export class InventoryDetailComponent implements OnInit {
  @Input() si: InventorySelectable | undefined;
  @Output() changedSelection: EventEmitter<number> = new EventEmitter<number>();
  errorMessage: string;
  constructor() { }

  ngOnInit() {
  }

  saveDisabled() {
    if (!this.si) { return true; }
    if (this.si.KernelsAvailable < 0) {
      this.errorMessage = "There not enough supply for selected Request(s). Require additional " + this.si.KernelsAvailable + " Kernels";
      setTimeout(() => { this.errorMessage = null; }, 800);
      return true;
    }
    return false;
  }
  requestChangeOn(id: number): void {
    this.changedSelection.emit(id);
  }
}
