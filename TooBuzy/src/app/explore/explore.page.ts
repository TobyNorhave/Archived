import { Component, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/firestore';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-explore',
  templateUrl: './explore.page.html',
  styleUrls: ['./explore.page.scss']
})
export class ExplorePage implements OnInit {
  restaurant: any;

  constructor(private afs: AngularFirestore) { }

  ngOnInit() {
    this.getAllRestaurants().subscribe(data => {
      this.restaurant = data;
      console.log(this.restaurant);
    });
  }

  getAllRestaurants(): Observable<any> {
    return this.afs.collection<any>('restaurants').valueChanges();
  }
}
