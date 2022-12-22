import { Component } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-addfood',
  templateUrl: './addfood.component.html',
  styleUrls: ['./addfood.component.css']
})
export class AddfoodComponent {

  availabilitytrue = true;
  availabilityfalse = false;
  availability:any;
  constructor(private adminService:AdminService){}

  handleAddFood(form:any){
    this.availability = form.value.availability === 'true';
    console.log(this.availability);
    form.value.availability = this.availability;
    console.log(form.value);
    this.adminService.addFood(form.value).subscribe(res =>{
      console.log(res);
    })
  }
}
