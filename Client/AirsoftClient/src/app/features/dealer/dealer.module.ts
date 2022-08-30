import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DealerRoutingModule } from './dealer-routing.module';
import { RegistrationComponent } from './registration/registration.component';
import { ProfileComponent } from './profile/profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EditComponent } from './edit/edit.component';


@NgModule({
  declarations: [
    RegistrationComponent,
    ProfileComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DealerRoutingModule,
    FormsModule,
    MatProgressSpinnerModule
  ]
})
export class DealerModule { }
