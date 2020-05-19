import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SliderComponent } from './slider/slider.component';
import { IonicModule } from '@ionic/angular';
import { CuisineComponent } from './cuisine/cuisine.component';
import { RestaurantsComponent } from './restaurants/restaurants.component';

@NgModule({
  declarations: [SliderComponent, CuisineComponent, RestaurantsComponent],
  exports: [SliderComponent, CuisineComponent, RestaurantsComponent],
  imports: [CommonModule, IonicModule]
})
export class ComponentModule {}
