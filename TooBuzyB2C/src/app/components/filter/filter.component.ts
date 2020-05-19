import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {

  price = [true, false, false];
  rating = [true, false];
  distance = [true, false];
  previous = [0, 0, 0];
  constructor() { }

  ngOnInit() { }

  changePrice(id) {
    this.price[id] = true;
    this.price[this.previous[0]] = false;
    this.previous[0] = id;
  }
  changeRating(id) {
    this.rating[id] = true;
    this.rating[this.previous[1]] = false;
    this.previous[1] = id;
  }
  changeDistance(id) {
    this.distance[id] = true;
    this.distance[this.previous[2]] = false;
    this.previous[2] = id;
  }

}
