import { Component } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent {

  constructor(private adminService:AdminService){}

  handleAddLocation(form:any){
    this.adminService.addLocation(form.value).subscribe(res =>{
      console.log(res);
    })
  }
}
