import { Component, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/firestore';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss']
})
export class ProfilePage implements OnInit {

  userInfo: any;

  constructor(private afs: AngularFirestore, private user: UserService) {  }

  ngOnInit() {
    this.getAllRestaurants().subscribe(data => {
      this.userInfo = data;
      console.log(this.userInfo);
    });
  }

  getAllRestaurants(): Observable<any> {
    return this.afs.collection<any>(`users/${this.user.getUID}`).valueChanges();
  }
}
