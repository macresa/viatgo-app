import { Component, inject } from '@angular/core';
import { Flight } from '../../models/flight';
import { FlightService } from '../../services/flight.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Booking } from '../../models/booking';
import jwtDecode from 'jwt-decode';
import { CheckoutService } from '../../services/checkout.service';

interface Token {
  unique_name: string;
}

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent {
  vueloIda: Flight = null!;
  vueloVuelta: Flight = null!;
  token: string = '';
  decoded: Token = null!;

  oneWay: boolean = false;

  private flightService = inject(FlightService);
  private checkoutService = inject(CheckoutService);
  
  constructor(private cookieService: CookieService, private router: Router){ 
    this.token = cookieService.get('token');
    this.decoded = jwtDecode<Token>(this.token);
  }
  
  ngOnInit(){
    this.flightService.getIda$.subscribe((v) => this.vueloIda = v);
    this.flightService.getVuelta$.subscribe((v) => this.vueloVuelta = v);
    this.oneWay = this.flightService.isOneWay;
  }
  
  checkOut(){
    const params: Booking = {
      idDeparture: this.vueloIda!.id,
      userName: this.decoded.unique_name
    }
  
    if(this.vueloVuelta != null){
      params.idReturn = this.vueloVuelta.id;
    }

    if(this.vueloIda != null){
      this.checkoutService.checkOut(params).subscribe();

      setTimeout(() => {
      this.router.navigate(['/myflight']);
      }, 500);
    }
  }
}
