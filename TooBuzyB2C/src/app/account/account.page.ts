import { Component, OnInit } from '@angular/core';
import { NavController, PopoverController } from '@ionic/angular';
import { NotificationsComponent } from '../components/notifications/notifications.component';

@Component({
  selector: 'app-account',
  templateUrl: './account.page.html',
  styleUrls: ['./account.page.scss'],
})
export class AccountPage implements OnInit {

  constructor(public nav: NavController, public popoverController: PopoverController) { }

  ngOnInit() {
  }


  openProfile(){
    this.nav.navigateForward('profile');
  }

  openPayment(){
    this.nav.navigateForward('payment');
  }

  async presentPopover(ev: any) {
    const popover = await this.popoverController.create({
      component: NotificationsComponent,
      event: ev,
      translucent: true
    });
    return await popover.present();
  }
}
