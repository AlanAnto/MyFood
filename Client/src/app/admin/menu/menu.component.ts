import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  menu : any;

  constructor (private adminService:AdminService, private route:Router){}

  ngOnInit(){
    this.adminService.getMenu().subscribe((res:any)=>{
      console.log(res);
      this.menu = res.data;
    })
  }
  
  handleDeleteFood(id:any){
    this.adminService.deleteFood(id).subscribe(res =>{
      console.log(res);
      this.route.navigate(["/admin/menu"]);
    })
  }
}
