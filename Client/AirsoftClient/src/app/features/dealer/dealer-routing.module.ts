import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DealerGuard } from 'src/app/infrastructure/guards/dealer.guard';
import { AuthGuard } from 'src/app/infrastructure/guards/auth-guard';
import { ProfileComponent } from './profile/profile.component';
import { RegistrationComponent } from './registration/registration.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'register',
        component: RegistrationComponent,
        pathMatch: 'full'
      },
      {
        path: 'profile',
        component: ProfileComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard, DealerGuard]
      },
      {
        path: 'edit',
        component: EditComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard, DealerGuard]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerRoutingModule { }
