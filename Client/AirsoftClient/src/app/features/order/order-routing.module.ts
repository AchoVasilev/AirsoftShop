import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/infrastructure/guards/auth-guard';
import { ClientGuard } from 'src/app/infrastructure/guards/client.guard';
import { ClientComponent } from './client/client.component';
import { DetailsComponent } from './details/details.component';

const routes: Routes = [
  {
    path: 'orders',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'client',
        component: ClientComponent,
        pathMatch: 'full',
        canActivate: [ClientGuard]
      },
      {
        path: 'client/:id',
        component: DetailsComponent,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
