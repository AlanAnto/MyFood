import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path:'auth',loadChildren:()=>import('./auth/auth-routing.module').then(m=>m.AuthRoutingModule) },
  {path:'public',loadChildren:()=>import('./public/public-routing.module').then(m=>m.PublicRoutingModule) },
  {path:'food',loadChildren:()=>import('./food/food-routing.module').then(m=>m.FoodRoutingModule) }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
