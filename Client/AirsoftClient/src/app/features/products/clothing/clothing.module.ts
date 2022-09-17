import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClothingRoutingModule } from './clothing-routing.module';
import { CreateComponent } from './create/create.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CreateComponent
  ],
  imports: [
    CommonModule,
    ClothingRoutingModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule
  ]
})
export class ClothingModule { }
