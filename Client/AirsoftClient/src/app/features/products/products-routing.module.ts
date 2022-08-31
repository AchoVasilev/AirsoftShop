import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../../infrastructure/guards/auth-guard";
import { DealerGuard } from "../../infrastructure/guards/dealer.guard";
import { CreateComponent } from "./create/create.component";
import { DetailsComponent } from "../products/details/details.component";
import { GunListComponent } from "../products/gun-list/gun-list.component";
import { EditComponent } from "./edit/edit.component";
import { MineComponent } from "./mine/mine.component";

export const routes: Routes = [
  {
    path: 'guns',
    children: [
      {
        path: 'all',
        component: GunListComponent,
        pathMatch: 'full'
      },
      {
        path: 'create',
        component: CreateComponent,
        pathMatch: 'full',
        canActivate: [AuthGuard, DealerGuard]
      },
      {
        path: ':name/:id',
        component: DetailsComponent,
        pathMatch: 'full'
      },
      {
        path: 'edit/:id',
        component: EditComponent,
        pathMatch: 'full'
      },
      {
        path: 'mine',
        component: MineComponent,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }