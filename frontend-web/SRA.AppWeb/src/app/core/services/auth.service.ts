import { Injectable } from '@angular/core';
import {
  SocialAuthService,
  GoogleLoginProvider,
  SocialUser
} from '@abacritt/angularx-social-login';

import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private user: SocialUser | null = null;
  private token: string | null = null;

  constructor(
    private socialAuthService: SocialAuthService,
    private http: HttpClient,
    private router: Router
  ) {
    this.socialAuthService.authState.subscribe((user) => {
      this.user = user;
      const idToken = user?.idToken;
      this.loginBackend(idToken);
    });
  }

  signInWithGoogle(): void {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  signOut(): void {
    this.socialAuthService.signOut().then(() => {
      this.token = null;
      localStorage.removeItem('access_token');
      this.router.navigate(['/login']);
    });
  }

  private loginBackend(idToken: string | null): void {
    if (!idToken) return;

    this.http.post<any>('https://localhost:5001/api/Auth/GoogleLogin', { idToken })
      .subscribe({
        next: (res) => {
          this.token = res.token;
          localStorage.setItem('access_token', this.token ?? '');
          this.router.navigate(['/calendario']);
        },
        error: () => {
          alert('Login no autorizado. ¿Usas @iescomercio.com?');
          this.signOut();
        }
      });
  }

  getToken(): string | null {
    return this.token || localStorage.getItem('access_token');
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }
}
