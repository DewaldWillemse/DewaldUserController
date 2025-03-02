import { Component } from '@angular/core';
import { DatabaseUsersModel } from '../../../../../assets/models/UserModel/users.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppPagesApplicationComponent } from '../../../application/application.component';
import { AppComponent } from '../../../../app.component';

@Component({
  selector: 'app-pages-components-users-usersdetails',
  standalone: true,
  imports: [FormsModule,
    CommonModule
  ],
  templateUrl: './usersdetails.component.html',
  styleUrl: './usersdetails.component.scss'
})
export class AppPagesComponentsUsersUsersdetailsComponent {

  UserModel: DatabaseUsersModel = new DatabaseUsersModel();
  ViewState: boolean = true;

  constructor(private apc: AppComponent, private app: AppPagesApplicationComponent) {
    this.ViewState = this.app.UserProfileState;
    this.SetupPage();
    
  }

  async SetupPage() {

    this.apc.target = "UsersControl";
    this.apc.payload.action = "DecryptUserToken";
    this.apc.payload.payload = this.app.UserProfileData;
    
    await this.apc.post();

    let result = this.apc.rstModel;

    if (!result.status) {
      alert(result.message);
      return;
    }

    this.UserModel = result.payload;
    console.log(this.UserModel);

  }

  async UpdateProfile() {
    this.apc.target = "UsersControl";
    this.apc.payload.action = "UpdateUser";
    this.apc.payload.payload = this.UserModel;
    
    await this.apc.post();

    let result = this.apc.rstModel;

    if (!result.status) {
      alert(result.message);
      return;
    }

    alert("User Updated");

    this.app.DisplayPage = "Users";  

  }

  async DeleteProfile() {
      this.apc.target = "UsersControl";
      this.apc.payload.action = "DeleteSingleUser";
      this.apc.payload.payload = this.UserModel;
      
      await this.apc.post();
  
      let result = this.apc.rstModel;
  
      if (!result.status) {
        alert(result.message);
        return;
      }

      this.app.DisplayPage = "Users";  
  
    }



}
