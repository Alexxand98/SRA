import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AuthService } from '../core/services/auth.service';
import { ReservaService } from '../core/services/reserva.service';
import { ProfesorService } from '../core/services/profesor.service';

import { CreateReservaDTO } from '../models/create-reserva.dto';
import { ReservaDTO } from '../models/reserva.dto';
import { ProfesorDTO } from '../models/profesor.dto';

@Component({
  selector: 'app-calendario',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h1>Calendario de reservas</h1>

    <label>Fecha:
      <input type="date" [(ngModel)]="nuevaReserva.fecha" />
    </label>

    <label>Franja horaria:
      <select [(ngModel)]="nuevaReserva.franjaHorariaId">
        <option [value]="1">08:00 - 09:00</option>
        <option [value]="2">09:00 - 10:00</option>
        <option [value]="3">10:00 - 11:00</option>
      </select>
    </label>

    <label>Grupo:
      <input type="text" [(ngModel)]="nuevaReserva.grupo" />
    </label>

    <button (click)="crearReserva()">Reservar</button>

    <h2>Mis reservas</h2>
    <ul>
      <li *ngFor="let r of reservas">
        {{ r.fecha | date: 'mediumDate' }} - Franja {{ r.franjaHorariaId }} - {{ r.grupo }} -
        <strong>{{ r.estado }}</strong>
      </li>
    </ul>
  `
})
export class CalendarioComponent implements OnInit {
  reservas: ReservaDTO[] = [];
  nuevaReserva: CreateReservaDTO = {
    fecha: '',
    franjaHorariaId: 1,
    grupo: '',
    profesorId: 0
  };

  private appUserId: string | null = null;

  constructor(
    private authService: AuthService,
    private reservaService: ReservaService,
    private profesorService: ProfesorService
  ) {}

  ngOnInit(): void {
    this.appUserId = this.authService.getAppUserId();

    this.profesorService.getProfesores().subscribe({
      next: (profesores: ProfesorDTO[]) => {
        const prof = profesores.find(p => p.appUserId.toLowerCase() === this.appUserId?.toLowerCase());

        if (prof) {
          this.nuevaReserva.profesorId = prof.id;
          this.cargarReservas();
        } else {
          alert('No se encontró el profesor correspondiente al usuario autenticado.');
        }
      },
      error: () => alert('Error al cargar la lista de profesores.')
    });
  }

  cargarReservas(): void {
    this.reservaService.getMisReservas().subscribe({
      next: (res) => (this.reservas = res),
      error: () => alert('Error al cargar tus reservas.')
    });
  }

  crearReserva(): void {
    if (!this.nuevaReserva.fecha || !this.nuevaReserva.grupo) {
      alert('Todos los campos son obligatorios.');
      return;
    }

    this.reservaService.crearReserva(this.nuevaReserva).subscribe({
      next: () => {
        alert('Reserva enviada con éxito.');
        this.cargarReservas();
      },
      error: () => alert('Error al crear reserva. ¿Día no lectivo o franja ocupada?')
    });
  }
}
