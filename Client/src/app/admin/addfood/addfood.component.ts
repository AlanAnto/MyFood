import { Component } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-addfood',
  templateUrl: './addfood.component.html',
  styleUrls: ['./addfood.component.css']
})
export class AddfoodComponent {

  constructor(private adminService:AdminService){}

  handleAddFood(form:any){
    this.adminService.addFood(form.value).subscribe(res =>{
      console.log(res);
    })
  }
}
