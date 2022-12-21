import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';  
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { AuthenticationLayoutComponent } from './authentication-layout.component';
import { PublicModule } from '../public/public.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent,
    AuthenticationLayoutComponent,
    LogoutComponent
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    PublicModule,
    FormsModule,
    HttpClientModule
  ],
  exports: []
})
export class AuthenticationModule { }
