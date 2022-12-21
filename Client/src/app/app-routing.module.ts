import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { AuthenticationModule } from './authentication/authentication.module';



const routes: Routes = [
  {path:'authentication',loadChildren:() => import('./authentication/authentication-routing.module').then(m => m.AuthenticationRoutingModule)}
];



@NgModule({

  imports: [
    RouterModule.forRoot(routes),
    AuthenticationModule
  ],

  exports: [RouterModule]

})

export class AppRoutingModule { }