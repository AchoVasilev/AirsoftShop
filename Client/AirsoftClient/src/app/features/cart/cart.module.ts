import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CartRoutingModule } from './cart-routing.module';
import { CartComponent } from './cart/cart.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EmptyComponent } from './empty/empty.component';
import { DeliveryComponent } from './delivery/delivery.component';
import { SummaryComponent } from './summary/summary.component';
import { UserDataComponent } from './user-data/user-data.component';


@NgModule({
  declarations: [
    CartComponent,
    EmptyComponent,
    DeliveryComponent,
    SummaryComponent,
    UserDataComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CartRoutingModule,
    MatProgressSpinnerModule
  ]
})
export class CartModule { }
