import { Component } from '@angular/core';
import { FoodService } from '../services/food.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  menu : any;
  constructor (private foodService:FoodService){}
  
  ngOnInit(){
    this.foodService.getMenu().subscribe((res:any)=>{
      console.log(res);
      this.menu = res.data;
    })
  }  
}
