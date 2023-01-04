import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent {

  constructor(private adminService:AdminService, private route:Router){}

  handleAddLocation(form:any){
    this.adminService.addLocation(form.value.location).subscribe(res =>{
      console.log(res);
      this.route.navigate(["/admin/home"]);
    })
  }
}
