import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProfesorDTO } from '../../models/profesor.dto';
import { Observable, map } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ProfesorService {
  private apiUrl = 'https://localhost:7001/api/Profesor';

  constructor(private http: HttpClient) {}

  getProfesores(): Observable<ProfesorDTO[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(res => res.result as ProfesorDTO[])
    );
  }
}
