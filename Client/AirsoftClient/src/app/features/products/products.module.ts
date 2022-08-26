import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { GunListComponent } from './gun-list/gun-list.component';
import { MineComponent } from './mine/mine.component';


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
    RouterModule,
    ProductsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatProgressSpinnerModule
  ]
})
export class ProductsModule { }
