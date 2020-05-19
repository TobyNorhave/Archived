import { Injectable } from '@angular/core';

interface Restaurant {
  name: string;
  uid: string;
}

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private restaurant: Restaurant;

  constructor() {}

  getUID() {
    return this.restaurant.uid;
  }
}
