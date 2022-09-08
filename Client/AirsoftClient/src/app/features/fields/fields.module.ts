import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FieldsRoutingModule } from './fields-routing.module';
import { CreateComponent } from './create/create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  declarations: [
    CreateComponent
  ],
  imports: [
    CommonModule,
    FieldsRoutingModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule
  ]
})
export class FieldsModule { }
