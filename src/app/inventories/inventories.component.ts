import { Component, OnInit } from '@angular/core';
import { SeedInventoryServiceService } from '../services/seed-inventory-service.service';
import { InventorySelectable } from '../inventory';

@Component({
  selector: 'app-inventories',
  templateUrl: './inventories.component.html',
  styleUrls: ['./inventories.component.css']
})
export class InventoriesComponent implements OnInit {
  public selectedsi: InventorySelectable;
  public siclean: InventorySelectable;
  sis: InventorySelectable[];
  pageTitle: string = 'Seed Inventory';
  errorMessage: string;
  resultMessage: string;
  constructor(private siservice: SeedInventoryServiceService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.siservice.getAvailableInventory().subscribe(
      items => {
        this.sis = items.map(i => new InventorySelectable(i));
        this.sis.forEach(i => {
          i.Selected = false;
          i.Included = false;
          i.Order = 0;
          i.KernelsUsed = i.InventoryRequests.reduce((sum, r) => sum + (r.Selected ? r.RequestedKernels : 0), 0);
          i.KernelsAvailable = i.Kernels - i.KernelsUsed;
        });
        this.sis.forEach((e, i) => {
          e.Order = i + 1;
        });
      },
      error => { this.errorMessage = <any>error; setTimeout(() => { this.errorMessage = null; }, 5000); });
  }
  onSelect(si: InventorySelectable): void {
    this.selectedsi = si;
  }
  onChangedSelection(siId: number): void {
    var item = this.sis.find(i => i.Id == siId);
    item.KernelsUsed = item.InventoryRequests.reduce((sum, i) => sum + (i.Selected ? i.RequestedKernels : 0), 0);
    item.KernelsAvailable = item.Kernels - item.KernelsUsed;
  }
}
