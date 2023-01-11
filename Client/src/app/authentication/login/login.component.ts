import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  message :  string = '';

  constructor(private authService:AuthenticationService, private route:Router){}

  handleLogin(form:any){
    console.log(form.value);
    this.authService.login(form.value).subscribe((res:any)=>{
      if(res.success){
        localStorage.setItem('token', res.data);
        let role=  JSON.parse(atob(res.data.split('.')[1]))['Role'];
        console.log(role);
        console.log(localStorage.getItem('token'));
        if(role=="User"){
          this.route.navigate(["/user/home"]);
        }
        else if(role=="Admin"){
          this.route.navigate(["admin/home"]);
        }
      }
    })
  }
}
