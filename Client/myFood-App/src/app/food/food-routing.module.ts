import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DisplayItemComponent } from './display-item/display-item.component';
import { ItemListComponent } from './item-list/item-list.component';

const routes: Routes = [
  {path:'food-item',component:DisplayItemComponent},
  {path:'item-list',component:ItemListComponent}
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FoodRoutingModule { }
