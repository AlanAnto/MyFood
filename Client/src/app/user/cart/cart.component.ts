import { Component } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  food : any;
  $index = 1;
constructor (private userService:UserService){}

ngOnInit(){
  this.userService.getCart().subscribe((res:any)=>{
    console.log(res);
    this.food = res;
  })
}
}
