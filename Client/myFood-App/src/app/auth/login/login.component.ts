import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private authService:AuthService,
    private router:Router,
    
  ){}
  message:string='';
  handleLogin(form: NgForm){
		console.log(form.value);
    return this.authService.login(form.value).subscribe((res:any)=>{
      if(res.success){
        localStorage.setItem('token', res.data);
        alert("Login successfull");
        this.router.navigate(["/"]);
      }
    })
	}
}
