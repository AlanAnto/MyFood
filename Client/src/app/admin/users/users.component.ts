import { Component } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent 
{
    users : any;

  constructor (private adminService:AdminService){}
  
  ngOnInit(){
    this.adminService.getUsers().subscribe((res:any)=>{
      console.log(res);
      this.users = res;
    })
  }
  
}
