// Interface für Hardware
export interface Hardware {
  id: string;
  name: string;
  amount: number;
  status: boolean;
}
// Interface für Events
export interface Event {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
}
// Interface für die Reservierungsresponse
export interface ReservationResponse {
  message: string;
  reservationDetails?: {
    eventId: string;
    hardwareIds: string[];
    startDate: Date;
    endDate: Date;
  };
}
// Interface für die Reservierung
export interface Reservation {
  id: string;
  eventId: string;
  eventName: string;
  hardwareNames: string[];
  quantities: number[];
  hardwareIds: string[];
  startDate: Date;
  endDate: Date;
}
