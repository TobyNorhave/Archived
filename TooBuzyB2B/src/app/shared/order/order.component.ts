import { Component, OnInit } from '@angular/core';
import { ModalController, NavController } from '@ionic/angular';
import { OrderDetailsComponent } from '../order-details/order-details.component';
import { OrderDetailsPage } from 'src/app/order-details/order-details.page';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
})
export class OrderComponent implements OnInit {

  constructor(private modalController: ModalController, private nav: NavController) { }

  ngOnInit() {}

  async presentOrder() {
    const modal = await this.modalController.create({
      component: OrderDetailsComponent
    });
    return await modal.present();
  }

}
