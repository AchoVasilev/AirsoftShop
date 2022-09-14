import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/infrastructure/guards/auth-guard';
import { DealerGuard } from 'src/app/infrastructure/guards/dealer.guard';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { GunListComponent } from './gun-list/gun-list.component';
import { MineComponent } from './mine/mine.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'all',
        component: GunListComponent,
      },
      {
        path: ':name',
        component: GunListComponent,
      },
      {
        path: 'create',
        component: CreateComponent,
        canActivate: [AuthGuard, DealerGuard]
      },
      {
        path: ':name/:id',
        component: DetailsComponent,
      },
      {
        path: 'edit/:id',
        component: EditComponent,
      },
      {
        path: 'mine',
        component: MineComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GunsRoutingModule { }
