import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FieldsRoutingModule } from './fields-routing.module';
import { CreateComponent } from './create/create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DetailsComponent } from './details/details.component';


@NgModule({
  declarations: [
    CreateComponent,
    DetailsComponent
  ],
  imports: [
    CommonModule,
    FieldsRoutingModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule
  ]
})
export class FieldsModule { }
