import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { AuthenticationModule } from './authentication/authentication.module';
import { PublicLayoutComponent } from './public/public-layout.component';
import { PublicModule } from './public/public.module';



const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./public/public-routing.module')
    .then(m => m.PublicRoutingModule) ,
    
  },
  { 
    path: 'authentication', 
    loadChildren: () => import('./authentication/authentication-routing.module')
    .then(m => m.AuthenticationRoutingModule) 
  },
  
];

@NgModule({

  imports: [
    RouterModule.forRoot(routes),
  ],

  exports: [RouterModule]

})

export class AppRoutingModule { }