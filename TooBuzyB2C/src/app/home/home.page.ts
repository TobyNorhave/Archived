import { Component } from '@angular/core';
import { NavController, PopoverController } from '@ionic/angular';
import { FilterComponent } from '../components/filter/filter.component';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(private nav: NavController, public popoverController: PopoverController) {}

  openDetails(){

    this.nav.navigateForward('restaurant-details');
  }
  

  async presentPopover(ev: any) {
    const popover = await this.popoverController.create({
      component: FilterComponent,
      event: ev,
      translucent: true
    });
    return await popover.present();
  }

}
   