import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CartRoutingModule } from './cart-routing.module';
import { CartComponent } from './cart/cart.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EmptyComponent } from './empty/empty.component';
import { DeliveryComponent } from './delivery/delivery.component';


@NgModule({
  declarations: [
    CartComponent,
    EmptyComponent,
    DeliveryComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CartRoutingModule,
    MatProgressSpinnerModule
  ]
})
export class CartModule { }
