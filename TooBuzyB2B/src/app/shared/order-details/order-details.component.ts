import { Component, OnInit } from '@angular/core';
import { ModalController, NavController } from '@ionic/angular';


@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss'],
})
export class OrderDetailsComponent implements OnInit {

  constructor(private modalController: ModalController, private nav: NavController) { }

  ngOnInit() {}

  closeModal() {
    this.modalController.dismiss();
  }
}
