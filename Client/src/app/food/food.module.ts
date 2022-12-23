import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FoodRoutingModule } from './food-routing.module';
import { MenuComponent } from './menu/menu.component';
import { SearchComponent } from './search/search.component';
import { PublicModule } from '../public/public.module';
import { FoodLayoutComponent } from './food-layout.component';


@NgModule({
  declarations: [
    MenuComponent,
    SearchComponent,
    FoodLayoutComponent
  ],
  imports: [
    CommonModule,
    FoodRoutingModule,
    PublicModule
  ]
})
export class FoodModule { }
