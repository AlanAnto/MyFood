import { Component } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-fooditem',
  templateUrl: './fooditem.component.html',
  styleUrls: ['./fooditem.component.css']
})
export class FooditemComponent {

  id : any;
  constructor(private userService:UserService){ }

  ngOnInit(){
    this.userService.getFood(this.id).subscribe(res=>{
      console.log(res)
    })
  }
}
