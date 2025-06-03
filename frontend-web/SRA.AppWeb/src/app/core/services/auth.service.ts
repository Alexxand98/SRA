import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private token: string | null = null;
  private userId: string | null = null;
  private email: string | null = null;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  handleCredentialResponse(response: any): void {
    const tokenId = response?.credential;
    if (!tokenId) return;

    this.http.post<any>('https://localhost:7001/api/User/google-login', { tokenId })
      .subscribe({
        next: (res) => {
          const result = res.result;
          this.token = result.token ?? '';

          const decoded: any = jwtDecode(this.token ?? '');
          this.userId = decoded?.nameidentifier ?? '';
          this.email = decoded?.email ?? '';

          localStorage.setItem('access_token', this.token ?? '');
          localStorage.setItem('app_user_id', this.userId ?? '');
          localStorage.setItem('user_email', this.email ?? '');

          this.router.navigate(['/calendario']);
        },
        error: () => {
          alert('Login no autorizado. ¿Usas @iescomercio.com o tu correo válido?');
          this.signOut();
        }
      });
  }

  getToken(): string | null {
    return this.token ?? localStorage.getItem('access_token');
  }

  getAppUserId(): string | null {
    return this.userId ?? localStorage.getItem('app_user_id');
  }

  getEmail(): string | null {
    return this.email ?? localStorage.getItem('user_email');
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  signOut(): void {
    this.token = null;
    this.userId = null;
    this.email = null;
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
