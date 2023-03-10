import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from './cart/cart.component';
import { FooditemComponent } from './fooditem/fooditem.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { UserLayoutComponent } from './user-layout.component';

const routes: Routes = [
  {
    path:'user',component:UserLayoutComponent,children:[
      {path:'home',component:HomeComponent},
      {path:'menu',component:MenuComponent},
      {path:'fooditem/:id',component:FooditemComponent},
      {path:'cart',component:CartComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }