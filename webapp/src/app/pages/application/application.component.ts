import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AppPagesComponentsUsersUsersdetailsComponent } from '../components/users/usersdetails/usersdetails.component';
import { AppPagesComponentsUsersUserslistComponent } from '../components/users/userslist/userslist.component';
import { DBUsersListModel } from '../../../assets/models/UserModel/users.model';


@Component({
  selector: 'app-pages-application',
  standalone: true,
  imports: [
    CommonModule,
    AppPagesComponentsUsersUserslistComponent,
    AppPagesComponentsUsersUsersdetailsComponent

  ],
  templateUrl: './application.component.html',
  styleUrl: './application.component.scss'
})
export class AppPagesApplicationComponent {

  MenuDisplay: string[] = [];
  MainMenu: string[] = ["Admin", "Configurations", "Tools", "Reports"];
  AdminMenu: string[] = ["Main Menu", "Users", "Server Settings", "General Settings", "Change Password"];
  ConfigurationsMenu: string[] = ["Main Menu", "Installation Codes", "User Codes", "Sub Services", "Nominated Accounts", "Source Mailbox Settings", "User Code Mapping", "Routing Table", "Entry Classes", "Unpaid Reason Codes"];
  ToolsMenu: string[] = ["Main Menu", "View Payments", "View Batches", "View Lurking Recalls", "View Blacklist Payments", "Capture Payments", "Capture Batches", "CDV Checking", "User Information", "View AuditLog"];
  ReportsMenu: string[] = ["Main Menu", "Daily Report", "Report Settings"];

  // DisplayPage: string = "nada";
  DisplayPage: string = "Landing";

  UserProfileState:boolean = true;
  UserProfileData: DBUsersListModel = new DBUsersListModel();

  constructor() {
    this.MenuDisplay = this.MainMenu;
    // this.DisplayPage = "Operators";

    // JABS Dev Remove this
    this.MenuDisplay = this.AdminMenu;
    this.DisplayPage = "Users";
  }


  ChangeView(navigateTo: string) {

    switch(navigateTo) {
      case "Admin":
        this.MenuDisplay = this.AdminMenu;
        break;
      case "Configurations":
        this.MenuDisplay = this.ConfigurationsMenu;
        break;
      case "Tools":
        this.MenuDisplay = this.ToolsMenu;
        break;
      case "Reports":
        this.MenuDisplay = this.ReportsMenu;
        break;
      case "Main Menu":
        this.MenuDisplay = this.MainMenu;
        this.DisplayPage = "Landing";
        break;
      default:
        this.DisplayPage = navigateTo;
        break;
    }

  }

}
