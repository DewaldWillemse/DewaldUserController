import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppComponentsHeaderComponent } from "./components/header/header.component";
import { CommonModule } from '@angular/common';
import { RestService } from '../assets/services/rest.service';
import { UiRequestModel } from '../assets/models/uihttp.models';
import { HttpClientModule } from '@angular/common/http';
import { DatabaseUsersModel } from '../assets/models/UserModel/users.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    AppComponentsHeaderComponent,
    HttpClientModule 
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  IsHomePage: boolean = false;
  UserModel: DatabaseUsersModel = new DatabaseUsersModel();
  rts: any;
  rstModel: any;
  payload: UiRequestModel = new UiRequestModel();
  target = "";

  constructor(private rst: RestService) {
    
  } 

  get() {

    return new Promise((resolve, reject) => {
      this.rst.sendGetRequest(this.target).toPromise().then((data: any) => {
        this.rstModel = data;            
        resolve(this.rstModel);
      });            
    })
    
  }
  
  post() {
    
    return new Promise((resolve, reject) => {
      this.rst.sendPostRequest(this.target, this.payload).toPromise().then((data: any) => {            
        this.rstModel = data;            
        resolve(this.rstModel);
      });
    })

  }
}
