import { Injectable } from '@angular/core';
import { Flight } from '../models/flight';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightService {
  vueloIda: Flight = null!;
  vueloVuelta: Flight = null!;

  isOneWay: boolean = false;

  private ida$ = new BehaviorSubject<Flight>(this.vueloIda);
  private vuelta$ = new BehaviorSubject<Flight>(this.vueloVuelta);

  constructor() { }

  get getIda$(): Observable<Flight>
  {
   return this.ida$.asObservable();
  }

  setVueloIda(flight: Flight)
  {
  this.ida$.next(flight);
  }

  get getVuelta$(): Observable<Flight>
  {
   return this.vuelta$.asObservable();
  }

  setVueloVuelta(flight: Flight)
  {
  this.vuelta$.next(flight);
  }
}
