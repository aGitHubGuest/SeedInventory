import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryDetailRequestsComponent } from './inventory-detail-requests.component';

describe('InventoryDetailRequestsComponent', () => {
  let component: InventoryDetailRequestsComponent;
  let fixture: ComponentFixture<InventoryDetailRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InventoryDetailRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryDetailRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
