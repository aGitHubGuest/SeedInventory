import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { InventoryRequestSelectable } from '../inventory';

@Component({
  selector: 'app-inventory-detail-requests',
  templateUrl: './inventory-detail-requests.component.html',
  styleUrls: ['./inventory-detail-requests.component.css']
})
export class InventoryDetailRequestsComponent implements OnInit {
  @Input() sirs: InventoryRequestSelectable[] | undefined;
  @Output() changedSelection: EventEmitter<number> = new EventEmitter<number>();
  constructor() { }

  ngOnInit() {
  }
  requestChangeOn(siid: number): void {
    this.changedSelection.emit(siid);
  }
}
