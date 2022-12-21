import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  message:string='';
  constructor(private authService : AuthService,
    private roter: Router){}
  handleSignup(form: NgForm){
    console.log(form.value);
		return this.authService.register(form.value).subscribe(res=>{
      console.log(res);
    });
	}
}
