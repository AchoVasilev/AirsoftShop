import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './shared/login/login.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';
import { UnderConstructionComponent } from './shared/under-construction/under-construction.component';

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
    path: 'cart',
    loadChildren: () => import('./features/cart/cart.module').then(x => x.CartModule)
  },
  {
    path: 'orders',
    loadChildren: () => import('./features/order/order.module').then(x => x.OrderModule)
  },
  {
    path: 'wishlist',
    loadChildren: () => import('./features/wish-list/wish-list.module').then(x => x.WishListModule)
  },
  {
    path: 'building',
    component: UnderConstructionComponent
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
