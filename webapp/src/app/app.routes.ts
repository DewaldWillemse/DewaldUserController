import { Routes } from '@angular/router';
import { AppPagesHomeComponent } from './pages/home/home.component';
import { AppPagesApplicationComponent } from './pages/application/application.component';

export const routes: Routes = [
    {path: '', redirectTo: '/home', pathMatch: 'full' },
    {path: 'home', component: AppPagesHomeComponent },
    {path: 'application', component: AppPagesApplicationComponent },
];
