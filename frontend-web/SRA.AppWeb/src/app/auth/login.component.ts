import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="login-container">
      <h2>Iniciar sesión</h2>
      <button (click)="login()" class="google-button">
        Iniciar sesión con Google
      </button>
    </div>
  `,
  styles: [`
    .login-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      margin-top: 80px;
    }

    .google-button {
      background-color: #4285f4;
      color: white;
      padding: 12px 24px;
      border: none;
      border-radius: 4px;
      font-size: 16px;
      cursor: pointer;
    }

    .google-button:hover {
      background-color: #3367d6;
    }
  `]
})
export class LoginComponent {
  constructor(private authService: AuthService, private router: Router) {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/calendario']);
    }
  }

  login(): void {
    this.authService.signInWithGoogle();
  }
}
