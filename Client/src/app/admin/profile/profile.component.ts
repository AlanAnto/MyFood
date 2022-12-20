import { Component } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'admin-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  constructor(private adminService:AdminService){}

  handleEdit(form:any)
  {
    console.log(form.value);
    this.adminService.editProfile(form.value).subscribe(res =>
      {
        console.log(res);
      });
  }
}
