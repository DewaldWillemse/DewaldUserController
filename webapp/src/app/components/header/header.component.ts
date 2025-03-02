import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-components-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class AppComponentsHeaderComponent {

  constructor(private route: Router, private apc: AppComponent) {
    
  }

  logout() 
  {
    //This is not right, should be something on the API side. serves purpose for now
    this.apc.IsHomePage = true;
    this.route.navigateByUrl("home");
  }
}
