import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  data:any;

  user = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: ''
  };

  constructor(private adminService:AdminService, private route:Router){}

  ngOnInit(){
    this.data = this.adminService.getProfile().subscribe((res:any)=>{
      this.data = res.data;
      this.user.firstName = this.data.firstName;
      this.user.lastName = this.data.lastName;
      this.user.email = this.data.email;
      this.user.phoneNumber = this.data.phoneNumber;
    })
  }

  handleEdit(form:any)
  {
    console.log(form.value);
    this.adminService.editProfile(form.value).subscribe(res =>
      {
        console.log(res);
        alert("Admin Details Updated");
        this.route.navigate(["admin/home"])
      }); 
  }

}
