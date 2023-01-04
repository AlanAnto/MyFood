import { Component } from '@angular/core';
import jwt_decode from "jwt-decode";


@Component({
  selector: 'public-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  ngOnInit(){
    var token:any = localStorage.getItem('token');
    var parsedToken = jwt_decode(token);
    console.log(parsedToken);
  }
}
