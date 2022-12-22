import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from './user-routing.module';
import { MenuComponent } from './menu/menu.component';
import { UserLayoutComponent } from './user-layout.component';
import { FormsModule } from '@angular/forms';
import { FooditemComponent } from './fooditem/fooditem.component';


@NgModule({
  declarations: [
    MenuComponent,
    UserLayoutComponent,
    FooditemComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    FormsModule
  ]
})
export class UserModule { }
