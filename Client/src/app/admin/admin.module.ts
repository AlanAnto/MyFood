import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProfileComponent } from './profile/profile.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UsersComponent } from './users/users.component';
import { MenuComponent } from './menu/menu.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { LocationComponent } from './location/location.component';
import { AdminLayoutComponent } from './admin-layout.component';
import { PublicModule } from '../public/public.module';
import { AddfoodComponent } from './addfood/addfood.component';


@NgModule({
  declarations: [
    HomeComponent,
    NavbarComponent,
    ProfileComponent,
    UsersComponent,
    MenuComponent,
    TransactionsComponent,
    LocationComponent,
    AdminLayoutComponent,
    AddfoodComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    HttpClientModule,
    PublicModule
  ]
})
export class AdminModule { }
