import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WishListRoutingModule } from './wish-list-routing.module';
import { WishListComponent } from './wish-list/wish-list.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  declarations: [
    WishListComponent
  ],
  imports: [
    CommonModule,
    WishListRoutingModule,
    MatProgressSpinnerModule
  ]
})
export class WishListModule { }
