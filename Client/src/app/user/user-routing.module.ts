import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooditemComponent } from './fooditem/fooditem.component';
import { MenuComponent } from './menu/menu.component';
import { UserLayoutComponent } from './user-layout.component';

const routes: Routes = [
  {
    path:'user',component:UserLayoutComponent,children:[
      {path:'menu',component:MenuComponent},
      {path:'fooditem/:id',component:FooditemComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }