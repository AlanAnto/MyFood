import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  constructor(private authService:AuthenticationService,private route : Router){}

  handleRegister(form:any)
  {
    console.log(form.value);
    this.authService.register(form.value).subscribe((res:any)=>{
      if(res.success){
        this.route.navigate(["/login"]);
      }
    });
  }
}
