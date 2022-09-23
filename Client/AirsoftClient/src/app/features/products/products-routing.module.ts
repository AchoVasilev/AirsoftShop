import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

export const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'guns',
        loadChildren: () => import('./guns/guns.module').then(m => m.GunsModule)
      },
      {
        path: 'clothings',
        loadChildren: () => import('./clothing/clothing.module').then(m => m.ClothingModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }