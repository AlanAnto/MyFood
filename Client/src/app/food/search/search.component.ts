import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FoodService } from '../services/food.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

  food:any;
  menu:any;
  
  constructor(private foodService:FoodService, private route:ActivatedRoute){}

  ngOnInit(){
    this.food = this.route.snapshot.paramMap.get('food');
    this.foodService.foodSearch(this.food).subscribe((res:any) =>{
      console.log(res);
      this.menu = res.data;
    });
  }
}
