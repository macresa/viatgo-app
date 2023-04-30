import { HttpParams } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Flight } from 'src/app/flights/models/flight';
import { ApiService } from 'src/app/flights/services/api.service';
import { FlightService } from 'src/app/flights/services/flight.service';

@Component({
  selector: 'app-searchresults',
  templateUrl: './searchresults.component.html',
  styleUrls: ['./searchresults.component.css']
})
export class SearchresultsComponent implements OnInit {
  
  loading: boolean = false;

  errorFound: boolean = false;
  errorFoundReturn: boolean = false;
  
  flightsDeparture: Flight[] = []!;
  flightsReturn: Flight[] = []!;

  vueloIda: Flight = null!;
  vueloVuelta: Flight = null!;

  idaLoaded: boolean = false;
  vueltaLoaded: boolean = false;

  from: string = '';
  destination: string = '';
  fromDate: Date = new Date();
  toDate: Date = new Date();

  private apiService = inject(ApiService);
  private flightService= inject(FlightService);
  private cdr = inject(ChangeDetectorRef);

  constructor(private router: Router) {}

ngOnInit(){
    this.from = localStorage.getItem('from')!;
    this.destination = localStorage.getItem('destination')!;
    this.fromDate = new Date(localStorage.getItem('fromDate')!);
    this.toDate = new Date(localStorage.getItem('toDate')!);
    this.callFlights();
}

callFlights(){
  this.from = localStorage.getItem('from')!;
  this.destination = localStorage.getItem('destination')!;
  this.fromDate = new Date(localStorage.getItem('fromDate')!);
  this.toDate = new Date(localStorage.getItem('toDate')!);

  this.changeState();
  
  if(this.vueloIda == null)
  {
    this.idaLoaded = false;
    this.getFlightsDeparture();
  }
  else
  {
    this.vueltaLoaded = false;
    this.getFlightsReturn();
  }
}
  changeState() { 
   this.loading = true;
   this.errorFound = false;
   this.errorFoundReturn = false;
   this.cdr.detectChanges(); 
 }

clickIda(vueloIda: Flight){
  this.vueloIda = vueloIda;
  this.flightService.setVueloIda(this.vueloIda);

  if(this.vueloIda != null)
  {
    if(this.flightService.isOneWay){
      this.router.navigate(['/checkout']);
      this.flightService.isOneWay = false;
      this.flightService.setVueloVuelta(null!);
    } else{
    this.getFlightsReturn();
    this.changeState();
    }
  }
}

clickVuelta(vueloVuelta: Flight){
  this.vueloVuelta = vueloVuelta;
  this.flightService.setVueloVuelta(vueloVuelta);
  this.router.navigate(['/checkout']);
}

duration(departure: Date, arrival: Date) {
  const mins = Math.round((new Date(arrival).getTime() - new Date(departure).getTime()) / 60000); 
  const hs = Math.floor(mins / 60);
  const min = mins % 60; 
  return `${hs.toString().padStart(2, '0')}h ${min.toString().padStart(2, '0')}m`; 
}

getFlightsDeparture(){
  let flightDeparture = new HttpParams();
  flightDeparture = flightDeparture.append("departure", this.from);
  flightDeparture = flightDeparture.append("arrival",this.destination);
  flightDeparture = flightDeparture.append("date",(this.fromDate!).toISOString());
 
  this.apiService.getFlightsDepartures(flightDeparture).subscribe({
    next: (v) => 
    {
      this.flightsDeparture = v, 
      this.errorFound = false;
      setTimeout(() => 
      { 
        this.loading = false;
        this.idaLoaded = true;
      }, 2000);
    },
    error: (e) => 
    {
      setTimeout(() => 
      {
      this.loading = false;
      this.errorFound = true;   
      this.idaLoaded = true;
      }, 2000);
    }
  });
  
}

getFlightsReturn(){
  let flightReturn = new HttpParams();
  flightReturn = flightReturn.append("departure",this.destination);
  flightReturn = flightReturn.append("arrival",this.from);
  flightReturn = flightReturn.append("date",(this.toDate).toISOString());

  this.apiService.getFlightsReturns(flightReturn).subscribe({
    next: (v) => 
    {
      this.flightsReturn = v,
      this.errorFoundReturn = false;
      setTimeout(() => 
      {
      this.loading = false;
      this.vueltaLoaded = true;
      }, 2000);
    },
    error: (e) => 
    {
      setTimeout(() => 
      {
      this.errorFoundReturn = true;
      this.loading = false; 
      this.vueltaLoaded = true;
      }, 2000);
    }
  });
}
}
