import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout.component';
import { HomeComponent } from './home/home.component';
import { LocationComponent } from './location/location.component';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  {
    path:'admin',component:AdminLayoutComponent, children:[
      {path:'home',component:HomeComponent},
      {path:'profile',component:ProfileComponent},
      {path:'users',component:UsersComponent},
      {path:'location',component:LocationComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
