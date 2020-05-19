import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';

interface User {
  email: string;
  uid: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private user: User;

  constructor(private afa: AngularFireAuth) {}

  setUser(user: User) {
    this.user = user;
  }

  getUID() {
    return this.user.uid;
  }
}
