import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FoodRoutingModule } from './food-routing.module';
import { DisplayItemComponent } from './display-item/display-item.component';
import { BrowserModule } from '@angular/platform-browser';
import { ItemListComponent } from './item-list/item-list.component';


@NgModule({
  declarations: [
    DisplayItemComponent,
    ItemListComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FoodRoutingModule
  ]
})
export class FoodModule { }
