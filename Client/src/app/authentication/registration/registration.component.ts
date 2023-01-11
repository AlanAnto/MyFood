import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  message : string = '';
  constructor(private authService:AuthenticationService, private route:Router){}

  handleRegister(form:any)
  {
    console.log(form.value);
    this.authService.register(form.value).subscribe(res => {
      console.log(res);
      this.route.navigate(["authentication/login"]);
    });
  }
}