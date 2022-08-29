import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderRoutingModule } from './order-routing.module';
import { ClientComponent } from './client/client.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  declarations: [
    ClientComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    MatProgressSpinnerModule
  ]
})
export class OrderModule { }
