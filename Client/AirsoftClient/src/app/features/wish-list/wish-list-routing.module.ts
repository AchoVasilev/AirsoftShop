import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/infrastructure/guards/auth-guard';
import { ClientGuard } from 'src/app/infrastructure/guards/client.guard';
import { WishListComponent } from './wish-list/wish-list.component';

const routes: Routes = [
  {
    path: '',
    component: WishListComponent,
    pathMatch: 'full',
    canActivate: [AuthGuard, ClientGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WishListRoutingModule { }
