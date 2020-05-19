import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { FilterComponent } from './components/filter/filter.component';
import { CartComponent } from './components/cart/cart.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { SupportComponent } from './components/support/support.component';
import { FeedbackComponent } from './components/feedback/feedback.component';

@NgModule({
  declarations: [AppComponent, FilterComponent, CartComponent, NotificationsComponent, SupportComponent, FeedbackComponent],
  entryComponents: [FilterComponent, CartComponent, NotificationsComponent, SupportComponent, FeedbackComponent],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
    StatusBar,
    SplashScreen,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
