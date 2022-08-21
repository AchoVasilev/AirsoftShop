import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientRoutingModule } from './client-routing.module';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ClientService } from 'src/app/services/client/client.service';
import { ProfileComponent } from './profile/profile.component';
import { EditComponent } from './edit/edit.component';


@NgModule({
  declarations: [
    RegistrationComponent,
    ProfileComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatProgressSpinnerModule
  ],
  providers: [
    ClientService
  ]
})
export class ClientModule { }
