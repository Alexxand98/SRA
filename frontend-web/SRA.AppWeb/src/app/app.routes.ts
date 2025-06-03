import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./auth/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'calendario',
    loadComponent: () => import('./pages/calendario.component').then(m => m.CalendarioComponent),
    canActivate: [() => import('./core/guards/auth.guard').then(m => m.authGuard)]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];
