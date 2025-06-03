import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateReservaDTO } from '../../models/create-reserva.dto';
import { ReservaDTO } from '../../models/reserva.dto';
import { Observable, map } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ReservaService {
  private apiUrlCrear = 'https://localhost:7001/api/ReservaProfesor'; // Endpoint correcto para crear reserva
  private apiUrlMisReservas = 'https://localhost:7001/api/ReservaProfesor'; // Para obtener reservas del profesor

  constructor(private http: HttpClient) {}

  getMisReservas(): Observable<ReservaDTO[]> {
    return this.http.get<any>(this.apiUrlMisReservas).pipe(
      map(res => res.result as ReservaDTO[])
    );
  }

  crearReserva(reserva: CreateReservaDTO): Observable<ReservaDTO> {
    return this.http.post<any>(this.apiUrlCrear, reserva).pipe(
      map(res => res.result as ReservaDTO)
    );
  }
}
