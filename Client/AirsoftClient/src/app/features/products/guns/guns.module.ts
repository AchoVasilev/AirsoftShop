import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GunsRoutingModule } from './guns-routing.module';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { GunListComponent } from './gun-list/gun-list.component';
import { MineComponent } from './mine/mine.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  declarations: [
    CreateComponent,
    DetailsComponent,
    EditComponent,
    GunListComponent,
    MineComponent
  ],
  imports: [
    CommonModule,
    GunsRoutingModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule
  ]
})
export class GunsModule { }
