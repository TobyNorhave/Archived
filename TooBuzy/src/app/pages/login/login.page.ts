import { Component, OnInit } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { appendFileSync } from 'fs';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss']
})
export class LoginPage implements OnInit {
  email = '';
  password = '';

  constructor(
    private afa: AngularFireAuth,
    private router: Router,
    public user: UserService,
    private alert: AlertController
  ) {}

  ngOnInit() {}

  async login() {
    const { email, password } = this;
    try {
      const res = await this.afa.auth.signInWithEmailAndPassword(
        email,
        password
      );

      if (res.user) {
        this.user.setUser({
          email,
          uid: res.user.uid
        });
        this.router.navigate(['/tabs']);
      }
    } catch (error) {
      console.dir(error);
      if (error.code === 'auth/user-not-found') {
        this.showAlert('Fejl!', 'Brugeren findes ikke i databasen!');
      } else {
        this.showAlert('Fejl!', 'E-mail eller kodeord er ikke korrekt!');
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
