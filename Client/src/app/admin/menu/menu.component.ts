import { Component } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  menu : any;
  constructor (private adminService:AdminService){}

  ngOnInit(){
    this.adminService.getMenu().subscribe((res:any)=>{
      console.log(res);
      this.menu = res;
    })
  }  
}
