export interface CreateReservaDTO {
  fecha: string;        // ISO date string, minúscula inicial
  grupo: string;
  profesorId: number;
  franjaHorariaId: number;
}
