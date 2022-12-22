import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  menu : any;
  constructor (private userService:UserService){}
  
  ngOnInit(){
    this.userService.getMenu().subscribe((res:any)=>{
      console.log(res);
      this.menu = res.data;
    })
  }  
}
