import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Router } from '@angular/router';
import { AngularFirestore } from '@angular/fire/firestore';

import { AlertController } from '@ionic/angular';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss']
})
export class RegisterPage implements OnInit {
  name: string;
  zip: string;
  phoneNo: string;
  email: string;
  password: string;
  cpassword: string;

  constructor(
    private afa: AngularFireAuth,
    private afs: AngularFirestore,
    public user: UserService,
    private alert: AlertController,
    public router: Router
  ) {}

  ngOnInit() {}

  async register() {
    const { name, zip, phoneNo, email, password, cpassword } = this;
    if (password !== cpassword) {
      this.showAlert('Fejl!', 'Koden matcher ikke');
    } else {
      try {
        const res = await this.afa.auth.createUserWithEmailAndPassword(
          email,
          password
        );
        this.afs.doc(`users/${res.user.uid}`).set({
          email,
          name,
          zip,
          phoneNo
        });
        this.user.setUser({
          email,
          uid: res.user.uid
        });
        this.showAlert('Succes!', 'Du er nu registreret!');
        try {
        this.router.navigate(['/tabs']);
        } catch (error) {
          console.dir(error);
        }

      } catch (error) {
        this.showAlert('Fejl!', error.message);
      }
    }
  }

  async showAlert(header: string, message: string) {
    const alert = await this.alert.create({
      header,
      message,
      buttons: ['Ok']
    });

    await alert.present();
  }
}
