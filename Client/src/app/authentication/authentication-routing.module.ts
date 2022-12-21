import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationLayoutComponent } from './authentication-layout.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
{
  path:'authentication',component:AuthenticationLayoutComponent, children:[
    {path:'login',component:LoginComponent},
    {path:'register',component:RegistrationComponent},
    {path:'logout',component:LogoutComponent}
  ]
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }
