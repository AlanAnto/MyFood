import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FoodLayoutComponent } from './food-layout.component';
import { MenuComponent } from './menu/menu.component';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
  {
    path:'',component:FoodLayoutComponent,children:[
      {path:'menu',component:MenuComponent},
      {path:'search/:food',component:SearchComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FoodRoutingModule { }
