import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './shared/login/login.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'products',
    loadChildren: () => import('./features/products/products.module').then(x => x.ProductsModule)
  },
  {
    path: 'dealer',
    loadChildren: () => import('./features/dealer/dealer.module').then(x => x.DealerModule)
  },
  {
    path: 'client',
    loadChildren: () => import('./features/client/client.module').then(x => x.ClientModule)
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
