import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {

  constructor(private nav:NavController) { }

  ngOnInit() {
  }


  register(){
    this.nav.navigateForward('home');
  }

  login(){
    this.nav.navigateBack('login');
  }
}
