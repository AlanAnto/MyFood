import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  constructor(private authService:AuthenticationService){}

  handleRegister(form:any)
  {
    console.log(form.value);
    this.authService.register(form.value).subscribe(res=>{
      console.log(res);
    });
  }
}
