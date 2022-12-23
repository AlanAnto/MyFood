import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from './user-routing.module';
import { MenuComponent } from './menu/menu.component';
import { UserLayoutComponent } from './user-layout.component';
import { FormsModule } from '@angular/forms';
import { FooditemComponent } from './fooditem/fooditem.component';
import { UserNavbarComponent } from './user-navbar/user-navbar.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    MenuComponent,
    UserLayoutComponent,
    FooditemComponent,
    UserNavbarComponent,
    HomeComponent,
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    FormsModule
  ]
})
export class UserModule { }
