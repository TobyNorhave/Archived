import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { OrderComponent } from './order/order.component';
import { OrderDetailsComponent } from './order-details/order-details.component';

@NgModule({
    imports: [CommonModule, IonicModule],
    declarations: [LoginComponent, ProfileComponent, OrderComponent, OrderDetailsComponent],
    exports: [LoginComponent, ProfileComponent, OrderComponent, OrderDetailsComponent],
})
export class SharedModule {}
