import { Component, OnInit } from '@angular/core';
import { getQueryPredicate } from '@angular/compiler/src/render3/view/util';
import { NavController, PopoverController } from '@ionic/angular';
import { CartComponent } from '../components/cart/cart.component';

@Component({
  selector: 'app-restaurant-details',
  templateUrl: './restaurant-details.page.html',
  styleUrls: ['./restaurant-details.page.scss'],
})
export class RestaurantDetailsPage implements OnInit {

  sections=[true, false, false];
  precat: any;
  categories = [{ name: "Sales", active: true, quantity: 1 }, { name: "Deals", active: false, quantity: 1 }, { name: "sub category", active: false, quantity: 0 }, { name: "sub category", active: false, quantity: 0 }, { name: "sub category", active: false, quantity: 0 }, { name: "sub category", active: false, quantity: 0 }, { name: "sub category", active: false, quantity: 0 }, { name: "sub category", active: false, quantity: 0 }];

  constructor(private nav: NavController,  public popoverController: PopoverController) {
    this.precat=this.categories[0];
   }

  ngOnInit() {
  }

  activate(category) {

    this.precat.active = false;
    category.active = true;
    this.precat = category;

  }

  changeSection(id){

    if(id==0){
      this.sections[0]=true;
      this.sections[1]=false;
      this.sections[2]=false;
    }else if(id==1){
      this.sections[0]=false;
      this.sections[1]=true;
      this.sections[2]=false;
    }else if(id==2){
      this.sections[0]=false;
      this.sections[1]=false;
      this.sections[2]=true;
    }
   
   
  }


  openCheckout(){
    this.nav.navigateForward('checkout');
  }
  openCart(){
    this.nav.navigateForward('cart');
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
