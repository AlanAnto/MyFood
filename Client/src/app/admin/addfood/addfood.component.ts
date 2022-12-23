import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-addfood',
  templateUrl: './addfood.component.html',
  styleUrls: ['./addfood.component.css']
})
export class AddfoodComponent {

  availability:any;
  foodType:number = 0;
  constructor(private adminService:AdminService, private route:Router){}

  handleAddFood(form:any){
    this.availability = form.value.availability === 'true';
    this.foodType = Number(form.value.foodType);
    form.value.availability = this.availability;
    form.value.foodType = this.foodType;
    console.log(form.value);
    this.adminService.addFood(form.value).subscribe(res =>{
      console.log(res);
      this.route.navigate(["/admin/menu"]);
    })
  }
}
