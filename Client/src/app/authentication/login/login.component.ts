import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private authService:AuthenticationService, private route:Router){}

  handleLogin(form:any){
    console.log(form.value);
    this.authService.login(form.value).subscribe((res:any)=>{
      if(res.success){
        localStorage.setItem('token', res.data);
        alert("Login successfull");
        this.route.navigate(["/"]);
      }
    })
  }
}
