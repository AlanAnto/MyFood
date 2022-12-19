import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DisplayItemComponent } from './display-item/display-item.component';

const routes: Routes = [
  {path:'food-item',component:DisplayItemComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FoodRoutingModule { }
