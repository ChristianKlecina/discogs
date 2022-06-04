import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {ShopComponent} from "./shop/shop.component";
import {TrackDetailsComponent} from "./shop/track-details/track-details.component";

const routes: Routes = [
  {path: '', component: HomeComponent, data:{breadcrumb: 'Home'}},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule), data:{breadcrumb: 'Shop'}},
  {path: 'admin', loadChildren: () => import('./admin-panel/admin-panel.module').then(mod => mod.AdminPanelModule), data:{breadcrumb: 'Admin'}},
  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule), data:{breadcrumb: 'Basket'}},
  {path: '**', redirectTo:'',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
