import { CreateReservaDTO } from './create-reserva.dto';

export interface ReservaDTO extends CreateReservaDTO {
  id: number;
  estado: string; // "Pendiente", "Aprobada", "Rechazada"
}
