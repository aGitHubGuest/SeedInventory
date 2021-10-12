export class Inventory {
  Id: number;
  Name: string;
  Kernels: number;
  KernelsUsed: number;
  KernelsAvailable: number;
  DateCreated?: Date;
  DateUpdated?: Date;
  InventoryRequests?: InventoryRequestSelectable[];
  constructor(item: Inventory) {
    this.Id = item.Id;
    this.Name = item.Name;
    this.Kernels = item.Kernels;
    this.KernelsUsed = item.KernelsUsed;
    this.KernelsAvailable = item.KernelsAvailable;
    this.InventoryRequests = [];
    item.InventoryRequests.forEach(i => this.InventoryRequests.push(new InventoryRequestSelectable(i)));
  }
};

export class InventorySelectable extends Inventory {
  Selected: boolean;
  Order?: number;
  Included: boolean;
};

export class InventoryRequest {
  Id: number;
  InventoryId: number;
  RequestedKernels: number;
  Approved: boolean;
  Locked: boolean;
  DateCreated?: Date;
  DateUpdated?: Date;
  Inventory?: Inventory;
  constructor(item: InventoryRequest) {
    this.Id = item.Id;
    this.InventoryId = item.InventoryId;
    this.RequestedKernels = item.RequestedKernels;
    this.Approved = item.Approved;
    this.Locked = item.Locked;
    if (item.Inventory) {
      this.Inventory = {
        Id: item.Inventory.Id,
        Name: item.Inventory.Name,
        Kernels: item.Inventory.Kernels,
        KernelsUsed: item.Inventory.KernelsUsed,
        KernelsAvailable: item.Inventory.KernelsAvailable,
        DateCreated: item.Inventory.DateCreated,
        DateUpdated: item.Inventory.DateUpdated
      }
    }
  };
};

export class InventoryRequestSelectable extends InventoryRequest {
  Selected: boolean;
  Order?: number;
  Included: boolean;
};


