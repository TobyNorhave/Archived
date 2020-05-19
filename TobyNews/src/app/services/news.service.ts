import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LoadingController } from '@ionic/angular';

import { tap, switchMap } from 'rxjs/operators';
import { defer } from 'rxjs';

const apiKey = environment.apiKey;
const apiUrl = environment.apiUrl;

const params = new HttpParams().set('apiKey', apiKey);


@Injectable({
  providedIn: 'root'
})
export class NewsService {
  loading;

  constructor(private http: HttpClient, public loadingController: LoadingController) { }

  async showLoading(){
    this.loading = await this.loadingController.create({
      duration: 10000,
      message: "Our monkeys are finding the right pages! Please hold!"
    });
    return await this.loading.present();
  };


  getData(url){
    return defer(this.showLoading.bind(this)).pipe(switchMap(data => {
    return this.http.get(`${apiUrl}/${url}`, {params}).pipe(tap(value => {
      this.loading.dismiss();
      console.log(value);
    }))
    }));
  }
}
