import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { InventoriesComponent } from './inventories/inventories.component';
import { InventoryDetailComponent } from './inventory-detail/inventory-detail.component';
import { InventoryDetailRequestsComponent } from './inventory-detail-requests/inventory-detail-requests.component';

@NgModule({
  declarations: [
    AppComponent,
    InventoriesComponent,
    InventoryDetailComponent,
    InventoryDetailRequestsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
