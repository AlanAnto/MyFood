import { Component } from '@angular/core';

@Component({
  selector: 'food-display-item',
  templateUrl: './display-item.component.html',
  styleUrls: ['./display-item.component.css']
})
export class DisplayItemComponent {
  quantity:number=0;
  dis:string="disabled:true";
  
 



  increaseQuantity(){
    this.quantity++;
    if(this.quantity>=4){
      console.log(this.quantity)
    }
    else{
      // this.disable=false;
    }
  }
  decreaseQuantity(){
    this.quantity--;
  }
}
