import { Component, AfterViewInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../core/services/auth.service';

declare const google: any;

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h2 style="text-align: center;">Iniciar sesión</h2>
    <div id="g_id_onload"
         data-client_id="702675750589-hogrurpogclmrogahgo6aj7bctpvnrrf.apps.googleusercontent.com"
         data-auto_prompt="false">
    </div>
    <div class="g_id_signin" data-type="standard"></div>
  `
})
export class LoginComponent implements AfterViewInit {
  private authService = inject(AuthService);
  private router = inject(Router);

  ngAfterViewInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/calendario']);
      return;
    }

    const script = document.createElement('script');
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.defer = true;
    script.onload = () => {
      google.accounts.id.initialize({
        client_id: '702675750589-hogrurpogclmrogahgo6aj7bctpvnrrf.apps.googleusercontent.com',
        callback: (response: any) => {
          this.authService.handleCredentialResponse(response);
        }
      });

      google.accounts.id.renderButton(
        document.querySelector('.g_id_signin'),
        { theme: 'outline', size: 'large' }
      );
    };

    document.head.appendChild(script);
  }
}
