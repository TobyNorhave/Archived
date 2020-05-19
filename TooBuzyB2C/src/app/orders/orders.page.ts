import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.page.html',
  styleUrls: ['./orders.page.scss'],
})
export class OrdersPage implements OnInit {

  
  sections=[true, false, false];
  constructor() { }

  ngOnInit() {
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

}
