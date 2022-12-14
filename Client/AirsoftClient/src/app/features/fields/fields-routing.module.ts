import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/infrastructure/guards/auth-guard';
import { DealerGuard } from 'src/app/infrastructure/guards/dealer.guard';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'create',
        component: CreateComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard, DealerGuard]
      },
      {
        path: ':id',
        component: DetailsComponent,
        pathMatch: 'full'
      },
      {
        path: 'edit/:id',
        component: EditComponent,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FieldsRoutingModule { }
