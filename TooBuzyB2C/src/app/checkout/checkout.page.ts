import { Component, OnInit } from '@angular/core';
import { CartComponent } from '../components/cart/cart.component';
import { PopoverController } from '@ionic/angular';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.page.html',
  styleUrls: ['./checkout.page.scss'],
})
export class CheckoutPage implements OnInit {

  constructor( public popoverController: PopoverController) { }

  ngOnInit() {
  }


  async presentPopover(ev: any) {
    const popover = await this.popoverController.create({
      component: CartComponent,
      event: ev,
      translucent: true
    });
    return await popover.present();
  }
}
