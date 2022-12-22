import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-fooditem',
  templateUrl: './fooditem.component.html',
  styleUrls: ['./fooditem.component.css']
})
export class FooditemComponent {

  id : any;
  quantity:number=0;
  dis:string="disabled:true";

  food = {
    id : 0,
    name : '',
    foodType : 0,
    description : '',
    price:0,
    availability:true
  }

  addedFood = {
    foodId:0,
    quantity:0,
    price:0
  }

  constructor(private userService:UserService, private route:ActivatedRoute){ }

  ngOnInit(){

    this.id = this.route.snapshot.paramMap.get('id');

    this.userService.getFood(this.id).subscribe((res:any)=>{
      this.food = res.data;
      this.addedFood.foodId = this.food.id;
      this.addedFood.price = this.food.price;
    });
  }

  handleAddToCart(data:any){
    console.log(data);  
    this.userService.addToCart(data).subscribe(res =>{
      console.log(res);
    })    
  }

  increaseQuantity(){
    this.quantity++;
    this.addedFood.quantity = this.quantity;
  }
  decreaseQuantity(){
    this.quantity--;
    this.addedFood.quantity = this.quantity;
}
}
