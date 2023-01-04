import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private route:Router){}

  search(form:any){
    console.log(form.value);
    this.route.navigate([`/search/${form.value.food}`]);
  }
}

