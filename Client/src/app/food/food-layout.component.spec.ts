import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodLayoutComponent } from './food-layout.component';

describe('FoodLayoutComponent', () => {
  let component: FoodLayoutComponent;
  let fixture: ComponentFixture<FoodLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodLayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
