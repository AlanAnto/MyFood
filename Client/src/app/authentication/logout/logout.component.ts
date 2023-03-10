import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent {

  constructor (private route:Router){}

  handleLogout(){
    localStorage.removeItem('token');
    alert("Logout successfull");
    this.route.navigate(["/"]);
  }
}
  