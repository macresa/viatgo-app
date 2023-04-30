export interface Flight
{
  id: number,
  airline: string,
  price: number,
  departureCity: string,
  departureTime: Date,
  arrivalCity: string,
  arrivalTime: Date
}